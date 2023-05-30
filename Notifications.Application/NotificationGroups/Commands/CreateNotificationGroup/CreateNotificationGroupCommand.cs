using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Entities;
using Notifications.Domain.Events.NotificationGroups;

namespace Notifications.Application.NotificationGroups.Commands.CreateNotificationGroup;

public record CreateNotificationGroupCommand : IRequest<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public string[]? Variables { get; set; }
    public string[]? SupportedUserLevel { get; set; }
    
    public Guid AppId { get; set; }
}

public class CreateNotificationGroupCommandHandler : IRequestHandler<CreateNotificationGroupCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheService _cacheService;
    private readonly IMassTransitService _massTransitService;

    public CreateNotificationGroupCommandHandler(IApplicationDbContext context, ICacheService cacheService, IMassTransitService massTransitService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        _massTransitService = massTransitService ?? throw new ArgumentNullException(nameof(massTransitService));
    }
    
    public async Task<Guid> Handle(CreateNotificationGroupCommand request, CancellationToken cancellationToken)
    {
        var notificationGroupExist = await _context.NotificationGroups
            .FirstOrDefaultAsync(u => u.Code.Contains(request.Code) || u.Code.Contains(request.Name), cancellationToken).ConfigureAwait(false);
        
        if (notificationGroupExist is not null)
            throw new BadRequestException("Notification group with given code or name already exists.");

        var app = await _context.Apps.FirstOrDefaultAsync(a => a.Id == request.AppId, cancellationToken).ConfigureAwait(false);
        if(app is null)
            throw new NotFoundException(nameof(App), request.AppId);

        if (request.ParentId.HasValue)
        {
            var parent = await _context.NotificationGroups
                .FirstOrDefaultAsync(n => n.Id == request.ParentId.Value, cancellationToken).ConfigureAwait(false);

            if (parent is null)
                throw new NotFoundException(nameof(NotificationGroup), request.ParentId);
        }
        
        var notificationGroupNew = new NotificationGroup()
        {
            Code = request.Code,
            Name = request.Name,
            ParentId = request.ParentId,
            Variables = request.Variables,
            SupportedUserLevel = request.SupportedUserLevel,
            AppId = request.AppId
        };

        // notificationGroupNew.DomainEvents.Add(new NotificationGroupCompletedEvent(notificationGroupNew));
        
        await _context.NotificationGroups.AddAsync(notificationGroupNew, cancellationToken).ConfigureAwait(false);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        _cacheService.RemoveData("notification-groups");
        
        _massTransitService.SendMessage<NotificationGroup>(notificationGroupNew);
        
        return notificationGroupNew.Id;
    }
}


