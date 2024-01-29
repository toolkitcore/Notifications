﻿using System.Collections;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Shared.Caching.Abstractions;

namespace Shared.Caching.InMemory;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cache"></param>
    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<bool> ExistsAsync(string key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        return await Task.FromResult(_cache.TryGetValue(key, out _));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="bool"></typeparam>
    public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null, bool keepTtl = false) where T : class
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        await SetStringAsync(key, JsonConvert.SerializeObject(value), expiry, keepTtl);

        return await ExistsAsync(key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiry"></param>
    /// <param name="keepTtl"></param>
    /// <returns></returns>
    public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null, bool keepTtl = false)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (expiry is null)
        {
            _cache.Set(key, value);
        }
        else
        {
            _cache.Set(key, value, expiry.Value);
        }

        return await ExistsAsync(key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(string key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        
        _cache.Remove(key);

        return !await ExistsAsync(key);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public async Task DeleteByPatternAsync(string pattern)
    {
        var allKey = GetAllCacheKeys();
        var removeKeys = allKey.Where(x => x.StartsWith(pattern)).ToList();
        if (removeKeys.Any())
        {
            foreach (var key in removeKeys)
            {
                await DeleteAsync(key);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public async Task<T> GetAsync<T>(string key) where T : class
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        var resultRaw = await GetStringAsync(key);
        if (string.IsNullOrWhiteSpace(resultRaw))
        {
            return default!;
        }

        return JsonConvert.DeserializeObject<T>(resultRaw);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<string> GetStringAsync(string key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (_cache.Get(key) is not string result)
        {
            return default!;
        }

        return await Task.FromResult(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async Task<bool> ReplaceAsync(string key, object value)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (await ExistsAsync(key) && !await DeleteAsync(key))
        {
            return false;
        }

        return await SetAsync(key, value);
    }

    #region [PRIVATE METHODS]
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private List<string> GetAllCacheKeys()
    {
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
        // var entries = _cache.GetType()?.GetField("_entries", flags)?.GetValue(_cache);
        // var cacheItems = entries as IDictionary;
        var typeOfCache = _cache.GetType();
        var fieldEntries = typeOfCache?.GetField("_entries", flags);
        var entriesValue = fieldEntries?.GetValue(_cache);
        var cacheItems = entriesValue as IDictionary;
        
        if (cacheItems is null)
            return default!;
        
        var keys = new List<string>();
        foreach (DictionaryEntry cacheItem in cacheItems)
        {
            keys.Add(cacheItem.Key.ToString());
        }
        return keys;
    }
    
    #endregion
}