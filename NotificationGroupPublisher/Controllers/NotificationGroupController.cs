using Microsoft.AspNetCore.Mvc;
using Shared.MessageBroker.Abstractions;
using Shared.Notification.MessageContracts;

namespace NotificationGroupPublisher.Controllers;

public class NotificationGroupController : ApiControllerBase
{
    private readonly IMessagePublisher _messagePublisher;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="messagePublisher"></param>
    public NotificationGroupController(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="publisher"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("api/notification--group-publisher")]
    public async Task<IActionResult> PublisherAsync([FromBody] PushNotificationGroupMessage publisher, CancellationToken cancellationToken = default)
    {
        await _messagePublisher.Publish(publisher, cancellationToken);
        return Content("Publisher notification group successful.");
    }

}