namespace Notifications.Domain.Events.NotificationGroups;

public class NotificationGroupCreatedEvent
{
    public NotificationGroup Item { get; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public NotificationGroupCreatedEvent(NotificationGroup item)
    {
        Item = item;
    }
    
}