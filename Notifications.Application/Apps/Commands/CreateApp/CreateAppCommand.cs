using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Entities;

namespace Notifications.Application.Apps.Commands.CreateApp;

public record CreateAppCommand : IRequest<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public string? SortName { get; set; }
}

public class CreateAppCommandHandler : IRequestHandler<CreateAppCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateAppCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<Guid> Handle(CreateAppCommand request, CancellationToken cancellationToken)
    {
        var appExist = await _context.Apps
            .FirstOrDefaultAsync(u => u.Code.Contains(request.Code) || u.Code.Contains(request.Name), cancellationToken).ConfigureAwait(false);
        
        if (appExist is not null)
            throw new BadRequestException("App with given code or name already exists.");

        var app = new App()
        {
            Code = request.Code,
            Name = request.Name,
            LogoUrl = request.LogoUrl ?? default!,
            SortName = request.SortName ?? default!
        };

        await _context.Apps.AddAsync(app, cancellationToken).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken);

        return app.Id;
    }
}