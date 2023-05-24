using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Entities;

namespace Notifications.Application.Apps.Commands.UpdateApp;

public class UpdateAppCommand : IRequest
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public string? SortName { get; set; }
}

public class UpdateAppCommandHandler : IRequestHandler<UpdateAppCommand>
{
    private readonly IApplicationDbContext _context;
    
    public UpdateAppCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(UpdateAppCommand request, CancellationToken cancellationToken)
    {
        var app = await _context.Apps
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken).ConfigureAwait(false);
        
        if (app is null)
            throw new NotFoundException(nameof(App), request.Id);

        var duplicate =
            await _context.Apps.FirstOrDefaultAsync(u => u.Id != request.Id && (u.Code == request.Code || u.Name == request.Name),
                    cancellationToken)
                .ConfigureAwait(false);
        
        if(duplicate != null)
            throw new NotFoundException("App is duplicated.");

        app.Code = request.Code;
        app.Name = request.Name;
        app.LogoUrl = request.LogoUrl;
        app.SortName = request.SortName;
        
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    }
}
