namespace Shared.MessageBroker.Abstractions;

public interface IMessagePublisher
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    Task Publish<T>(
        T message,
        CancellationToken cancellationToken = default,
        Dictionary<string, string>? metaData = null) where T : class;
}