using Notifications.Application.Common.Models.Abstractions;
using Notifications.Application.Common.Models.PaginatedList;
using Notifications.Application.NotificationGroups.Models;
using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User> GetByIdAsync(string Id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User> GetByUserNameAsync(string userName, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User> CreateAsync(User entity, CancellationToken cancellationToken);
}