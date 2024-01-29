using Microsoft.Extensions.Configuration;

namespace Shared.Utilities;

public static class ConfigurationExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static T GetOptions<T>(this IConfiguration configuration, string section) where T : class, new()
    {
        var options = new T();
        configuration.GetSection(section).Bind(options);
        return options;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static T GetOptions<T>(this IConfiguration configuration) where T : class, new()
        => GetOptions<T>(configuration, typeof(T).Name);
}