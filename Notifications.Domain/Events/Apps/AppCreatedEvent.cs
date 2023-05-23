namespace Notifications.Domain.Events.Apps;

public class AppCreatedEvent
{
    public AppCreatedEvent(App item)
    {
        Item = item;
    }

    public App Item { get; }
}