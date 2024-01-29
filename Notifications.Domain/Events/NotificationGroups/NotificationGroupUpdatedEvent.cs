namespace Notifications.Domain.Events.NotificationGroups;

public class NotificationGroupUpdatedEvent
{
    public NotificationGroup Item { get; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public NotificationGroupUpdatedEvent(NotificationGroup item)
    {
        Item = item;
    }
}