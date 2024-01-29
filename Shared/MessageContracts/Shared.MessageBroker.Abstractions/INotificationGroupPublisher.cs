namespace Shared.MessageBroker.Abstractions;

public interface INotificationGroupPublisher
{
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
    Task Publish(
        string code,
        string name,
        Guid? parentId,
        string[]? variables,
        string[]? supportedUserLevel,
        Guid appId,
        CancellationToken cancellationToken = default);
}