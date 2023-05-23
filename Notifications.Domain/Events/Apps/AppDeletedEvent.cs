namespace Notifications.Domain.Events.Apps;

public class AppDeletedEvent
{
    public App Item { get; }
    
    public AppDeletedEvent(App item)
    {
        Item = item;
    }

}