using MediatR;
using Microsoft.Extensions.Logging;
using Notifications.Application.Common.Models.Abstractions;
using Notifications.Domain.Events.NotificationGroups;

namespace Notifications.Application.NotificationGroups.EvenHandlers;

public class NotificationGroupCompletedEventHandler: INotificationHandler<DomainEventNotification<NotificationGroupCompletedEvent>>
{
    private readonly ILogger<NotificationGroupCompletedEventHandler> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public NotificationGroupCompletedEventHandler(ILogger<NotificationGroupCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task Handle(DomainEventNotification<NotificationGroupCompletedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}