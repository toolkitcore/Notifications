using Microsoft.Extensions.DependencyInjection;
using Shared.MessageBroker.Abstractions;

namespace Shared.Notification.MessageContracts;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddNotificationPublisher(this IServiceCollection services)
    {
        services.AddTransient<INotificationGroupPublisher>(sp =>
        {
            return new NotificationGroupGroupPublisher(sp.GetRequiredService<IMessagePublisher>());
        });
        return services;
    }
}