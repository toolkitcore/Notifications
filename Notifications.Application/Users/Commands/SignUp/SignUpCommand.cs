using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Common.Models.Users;
using Notifications.Domain.Entities;

namespace Notifications.Application.Users.Commands.SignUp;

public record SignUpCommand : IRequest<AuthenticationResult>
{
    public string UserName { get; set; }
    public string Code { get; set; }
    public string CountryCode { get; set; }
}

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, AuthenticationResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public SignUpCommandHandler(IApplicationDbContext context, IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
    }
    

    public async Task<AuthenticationResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        // 1. Validate the user doesn't exits
        var userExist = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName.Contains(request.UserName), cancellationToken).ConfigureAwait(false);
        
        if (userExist is not null)
            throw new BadRequestException("User with given UserName already exists.");
        
        // 2. Create user (generate unique Id) & Persist to database
        var user = new User()
        {
            UserName = request.UserName,
            Code = request.Code,
            CountryCode = request.CountryCode
        };

        await _context.Users.AddAsync(user, cancellationToken).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken);
        
        // 3. Create JWT token
        var token = _jwtTokenGenerator.Generate(user);

        return new AuthenticationResult(user, token);
    }
}
