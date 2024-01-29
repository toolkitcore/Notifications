namespace Notifications.Domain.Events.NotificationGroups;

public class NotificationGroupDeletedEvent
{
    public NotificationGroup Item { get; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public NotificationGroupDeletedEvent(NotificationGroup item)
    {
        Item = item;
    }
}