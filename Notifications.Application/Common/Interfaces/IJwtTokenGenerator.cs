using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}