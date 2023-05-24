using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Models.Users;

public record AuthenticationResult
(
    User User,
    string Token
);