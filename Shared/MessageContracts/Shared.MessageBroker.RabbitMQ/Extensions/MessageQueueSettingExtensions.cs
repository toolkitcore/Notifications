namespace Shared.MessageBroker.RabbitMQ.Extensions;

public static class MessageQueueSettingExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    public static string GetPublishEndpoint(this MessageQueueSetting setting, string endpoint)
    {
        var virHost = setting.VirtualHost.Trim().TrimStart('/').TrimEnd('/');
        virHost = string.IsNullOrWhiteSpace(virHost) ? virHost : $"/{virHost}";
        return $"rabbitmq://{setting.Host}:{setting.Port}{virHost}/{endpoint}";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    public static string GetPublishEndpointError(this MessageQueueSetting setting, string endpoint)
    {
        var virHost = setting.VirtualHost.Trim().TrimStart('/').TrimEnd('/');
        virHost = string.IsNullOrWhiteSpace(virHost) ? virHost : $"/{virHost}";
        return $"rabbitmq://{setting.Host}:{setting.Port}{virHost}/{endpoint}_error";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    public static string GetReceiveEndpoint(this MessageQueueSetting setting, string endpoint)
    {
        return $"{setting.QueuePrefix}{endpoint}";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    public static string GetReceiveEndpointError(this MessageQueueSetting setting, string endpoint)
    {
        return $"{setting.QueuePrefix}{endpoint}_error";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    public static Uri GetEndpointAddress(this MessageQueueSetting setting, string endpoint)
    {
        return new Uri(setting.GetPublishEndpoint(endpoint));
    }
}