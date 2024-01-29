using Shared.MessageBroker.Abstractions;
using Shared.Utilities;

namespace Shared.Notification.MessageContracts;

public class NotificationGroupGroupPublisher : INotificationGroupPublisher
{
    private readonly IMessagePublisher _publishEndpoint;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="publishEndpoint"></param>
    public NotificationGroupGroupPublisher(IMessagePublisher publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <param name="name"></param>
    /// <param name="parentId"></param>
    /// <param name="variables"></param>
    /// <param name="supportedUserLevel"></param>
    /// <param name="appId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Publish(string code,
        string name,
        Guid? parentId,
        string[]? variables,
        string[]? supportedUserLevel,
        Guid appId, 
        CancellationToken cancellationToken = default)
    {
        var message = new PushNotificationGroupMessage(
           code,
           name,
           parentId,
           variables,
           supportedUserLevel,
           appId);
        
        await _publishEndpoint.Publish(message, cancellationToken);
    }
    
}