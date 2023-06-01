using MassTransit;
using MediatR;
using Notifications.Application.Common.Interfaces;
using Shared.Notification.MessageContracts;

namespace Notifications.WebApi.Consumers;

public class PushNotificationGroupConsumer : IConsumer<PushNotificationGroupMessage>
{
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _dbContext;
    
    public async Task Consume(ConsumeContext<PushNotificationGroupMessage> context)
    {
        throw new NotImplementedException();
    }
}