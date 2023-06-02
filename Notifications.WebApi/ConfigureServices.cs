using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application;
using Notifications.Infrastructure;
using Notifications.WebApi.Consumers;
using Shared.Authorization.Extensions;
using ZymLabs.NSwag.FluentValidation;
using Shared.MessageBroker.RabbitMQ.Extensions;
using Shared.Notification.MessageContracts;
using NotificationTopic = Shared.Notification.MessageContracts.Constant;



namespace Notifications.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddHttpContextAccessor();
        
        services.AddControllers();

        services
            .AddApplicationServices()
            .AddInfrastructureServices(configuration);

        services.AddScoped<FluentValidationSchemaProcessor>(provider =>
        {
            var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddAuthentication(configuration, env);
        services.AddMessageQueue(configuration, env);

        return services;
    }

    private static IServiceCollection AddMessageQueue(this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {

        services.AddMasstransitRabbitMqMessagePublisher(
            configuration,
            (configurator, setting) =>
            {
                configurator.AddConsumer<PushNotificationGroupConsumer>();
            },
            (context, cfg, setting) =>
            {
                cfg.ReceiveEndpoint($"{setting.GetReceiveEndpoint(NotificationTopic.TopicConstant.PushNotificationGroup)}",
                    endpointConfigurator =>
                    {
                        endpointConfigurator.Bind($"{NotificationTopic.TopicConstant.PushNotificationGroup}");
                        endpointConfigurator.Bind<PushNotificationGroupMessage>();
                        endpointConfigurator.UseRetry(r => r.Interval(3, TimeSpan.FromSeconds(3)));
                        endpointConfigurator.UseRateLimit(5);
                        endpointConfigurator.ConfigureConsumer<PushNotificationGroupConsumer>(context);
                    });
                
                cfg.ConfigureEndpoints(context);
                
            }
        );
        services.AddMassTransitHostedService();
        return services;
    }

    private static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env
        )
    {
        services.AddJwtBearer(configuration, null);
        services.AddAuthorization(options =>
        {
            var defaultAuthorizationPolicyBuilder =
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);

            defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
            options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
        });
        return services;
    }
    
    
}