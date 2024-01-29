using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<string> Generate(User user);
}