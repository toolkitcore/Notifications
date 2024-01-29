namespace Shared.Caching.Abstractions;

public interface ICacheService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(string key);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="bool"></typeparam>
    Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null, bool keepTtl = false) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiry"></param>
    /// <param name="keepTtl"></param>
    /// <returns></returns>
    Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null, bool keepTtl = false);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(string key);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    Task DeleteByPatternAsync(string pattern);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    Task<T> GetAsync<T>(string key) where T : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<string> GetStringAsync(string key);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    Task<bool> ReplaceAsync(string key, object value);
}