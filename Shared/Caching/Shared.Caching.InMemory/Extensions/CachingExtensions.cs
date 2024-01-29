﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Shared.Caching.Abstractions;

namespace Shared.Caching.InMemory.Extensions;

public static class CachingExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInMemoryCaching(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheService>(s =>
            new MemoryCacheService(s.GetRequiredService<IMemoryCache>()));
        return services;
    }
}