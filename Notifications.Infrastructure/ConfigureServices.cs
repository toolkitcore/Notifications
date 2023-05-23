using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Domain.Configurations;
using Notifications.Infrastructure.Common.Extensions;
using Notifications.Infrastructure.Persistence;

namespace Notifications.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseSetting = configuration.GetOptions<DatabaseSetting>();
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseSetting.Default));

        services.AddScoped<ApplicationDbContextSeed>();
        
        return services;
    }
}