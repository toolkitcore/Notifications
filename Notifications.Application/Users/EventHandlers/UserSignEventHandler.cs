using MediatR;
using Microsoft.Extensions.Logging;
using Notifications.Domain.Events.Users;

namespace Notifications.Application.Users.EventHandlers;

public class UserSignEventHandler : INotificationHandler<UserSignInEvent>
{
    private readonly ILogger<UserSignEventHandler> _logger;

    public UserSignEventHandler(ILogger<UserSignEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserSignInEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}