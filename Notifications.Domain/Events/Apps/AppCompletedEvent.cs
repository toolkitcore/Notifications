using Notifications.Domain.Common.Events;

namespace Notifications.Domain.Events.Apps;

public class AppCompletedEvent : BaseEvent
{
    public App Item { get; }
    
    public AppCompletedEvent(App item)
    {
        Item = item;
    }
}