using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Configurations;
using Notifications.Infrastructure.Common.Extensions;
using StackExchange.Redis;


namespace Notifications.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _database;
    private readonly RedisSetting _redisSetting;

    public CacheService(IConfiguration configuration)
    {
        _redisSetting = configuration.GetOptions<RedisSetting>() ?? throw new ArgumentNullException(nameof(configuration));
        _database = ConnectionMultiplexer.Connect(_redisSetting.RedisUrl).GetDatabase();
        
    }
    public T GetData<T>(string key)
    {
        var value = _database.StringGet(key);
        if (!string.IsNullOrEmpty(value))
            return JsonConvert.DeserializeObject<T>(value);
        
        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var isSet = _database.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
        
        return isSet;
    }

    public object RemoveData(string key)
    {
        bool _isKeyExist = _database.KeyExists(key);
        
        if (_isKeyExist == true) 
            return _database.KeyDelete(key);
        
        return false;
    }
}