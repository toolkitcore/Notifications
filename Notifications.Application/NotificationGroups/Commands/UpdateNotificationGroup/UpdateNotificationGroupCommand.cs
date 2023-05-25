using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Entities;

namespace Notifications.Application.NotificationGroups.Commands.UpdateNotificationGroup;

public record UpdateNotificationGroupCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public string[]? Variables { get; set; }
    public string[]? SupportedUserLevel { get; set; }
    public Guid AppId { get; set; }
}

public class UpdateNotificationGroupCommandHandler : IRequestHandler<UpdateNotificationGroupCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateNotificationGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public async Task<Unit> Handle(UpdateNotificationGroupCommand request, CancellationToken cancellationToken)
    {
        var notificationGroup = await _context.NotificationGroups.FindAsync(new object[] { request.Id }, cancellationToken)
            .ConfigureAwait(false);
        if (notificationGroup is null)
            throw new NotFoundException(nameof(NotificationGroup), request.Id);

        var notificationGroupDuplicate = await _context.NotificationGroups
            .FirstOrDefaultAsync(
                u => (u.Code.Contains(request.Code) || u.Code.Contains(request.Name)) && u.Id != request.Id,
                cancellationToken).ConfigureAwait(false);

        if (notificationGroupDuplicate is not null)
            throw new BadRequestException("The notification group is duplicate.");

        var app = await _context.Apps.FirstOrDefaultAsync(a => a.Id == request.AppId, cancellationToken)
            .ConfigureAwait(false);
        if (app is null)
            throw new NotFoundException(nameof(App), request.AppId);

        if (request.ParentId.HasValue)
        {
            var parent = await _context.NotificationGroups
                .FirstOrDefaultAsync(n => n.Id == request.ParentId.Value, cancellationToken).ConfigureAwait(false);

            if (parent is null)
                throw new NotFoundException(nameof(NotificationGroup), request.ParentId);
        }

        notificationGroup.Code = request.Code;
        notificationGroup.Name = request.Name;
        notificationGroup.ParentId = request.ParentId;
        notificationGroup.Variables = request.Variables;
        notificationGroup.SupportedUserLevel = request.SupportedUserLevel;
        notificationGroup.AppId = request.AppId;

        _context.NotificationGroups.Update(notificationGroup);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
