using MediatR;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Entities;
using Notifications.Domain.Events.NotificationGroups;

namespace Notifications.Application.NotificationGroups.Commands.DeleteNotificationGroup;

public record DeleteNotificationGroupCommand(Guid groupId) : IRequest<Unit>;

public class DeleteNotificationGroupCommandHandler : IRequestHandler<DeleteNotificationGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteNotificationGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<Unit> Handle(DeleteNotificationGroupCommand request, CancellationToken cancellationToken)
    {
        var notificationGroup = await _context.NotificationGroups.FindAsync(new[] { request.groupId }, cancellationToken).ConfigureAwait(false);
        if(notificationGroup is null)
            throw new NotFoundException(nameof(NotificationGroup), request.groupId);

        _context.NotificationGroups.Remove(notificationGroup);
        
        notificationGroup.DomainEvents.Add(new NotificationGroupCompletedEvent(notificationGroup));
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}