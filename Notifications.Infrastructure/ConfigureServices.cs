using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Configurations;
using Notifications.Domain.Entities;
using Notifications.Infrastructure.Identity;
using Notifications.Infrastructure.Persistence;
using Notifications.Infrastructure.Repositories.NotificationGroups;
using Notifications.Infrastructure.Services;
using Shared.Caching.Redis.Extensions;
using Shared.Utilities;

namespace Notifications.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSetting = configuration.GetOptions<JwtSettings>();
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSetting.Issuer,
                    ValidAudience = jwtSetting.Audience
                };
            });
        
        var databaseSetting = configuration.GetOptions<DatabaseSetting>();
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseSetting.Default));

        services.AddScoped<ApplicationDbContextSeed>();
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        services
            .AddDefaultIdentity<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddSingleton<IMassTransitService, MassTransitService>();

        services.AddSingleton<IUserRepository, UserRedisRepository>();

        services.AddRedisCaching(configuration);
        
        return services;
    }
    
    private static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRedisCaching(configuration);
        return services;
    }
}