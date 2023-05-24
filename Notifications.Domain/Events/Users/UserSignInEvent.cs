using Notifications.Domain.Common.Events;

namespace Notifications.Domain.Events.Users;

public class UserSignInEvent : BaseEvent
{
    public User Item { get; }
    
    public UserSignInEvent(User item)
    {
        Item = item;
    }
}
