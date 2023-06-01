using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application;
using Notifications.Infrastructure;
using Shared.Authorization.Extensions;
using ZymLabs.NSwag.FluentValidation;
using Shared.MessageBroker.RabbitMQ.Extensions;


namespace Notifications.WebApi;

public static class ConfigureServices
{
    static ConfigureServices()
    {
    }

    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
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
                configurator.SetEndpointNameFormatter(new SnakeCaseEndpointNameFormatter(setting.QueuePrefix, false));
            },
            (context, cfg, setting) =>
            {
                
            }
        );
        return services;
    }

    private static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env
        )
    {
        services.AddJwtBearer(configuration);
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