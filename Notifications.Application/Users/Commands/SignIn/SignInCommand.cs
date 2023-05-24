using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Common.Models.Users;
using Notifications.Domain.Entities;

namespace Notifications.Application.Users.Commands.SignIn;

public class SignInCommand : IRequest<AuthenticationResult>
{
    public string UserName { get; set; }
    public string Code { get; set; }
    
}

public class SignUpCommandHandler : IRequestHandler<SignInCommand, AuthenticationResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public SignUpCommandHandler(IApplicationDbContext context, IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
    }
    

    public async Task<AuthenticationResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        // 1. Validate the user doesn't exits
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName.Contains(request.UserName), cancellationToken).ConfigureAwait(false);
        
        if (user is  null)
            throw new NotFoundException(nameof(User), request.UserName);
        
        if(user.Code == request.Code)
            throw new NotFoundException(nameof(User), request.Code);

        // 3. Create JWT token
        var token = _jwtTokenGenerator.Generate(user);

        return new AuthenticationResult(user, token);
    }
}
