using Notifications.Application.Common.Models.Abstractions;
using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Interfaces;

public interface IIdentityService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<IList<string>> GetRolesAsync(User user);
}