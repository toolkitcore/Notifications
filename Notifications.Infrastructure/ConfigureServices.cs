using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Configurations;
using Notifications.Infrastructure.Common.Extensions;
using Notifications.Infrastructure.Persistence;
using Notifications.Infrastructure.Services;

namespace Notifications.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseSetting = configuration.GetOptions<DatabaseSetting>();
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseSetting.Default));

        services.AddScoped<ApplicationDbContextSeed>();
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        return services;
    }
}