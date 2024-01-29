using Notifications.Domain.Common.Events;

namespace Notifications.Domain.Events.NotificationGroups;

public class NotificationGroupCompletedEvent : DomainEvent
{
    public NotificationGroup Item { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public NotificationGroupCompletedEvent(NotificationGroup item)
    {
        Item = item;
    }
}