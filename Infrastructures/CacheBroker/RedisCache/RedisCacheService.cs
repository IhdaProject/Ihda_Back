using CacheBroker.Interfaces;

namespace CacheBroker.RedisCache;

public class RedisCacheService : ICacheService
{
    public Task<T> GetDataAsync<T>(string key)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetTryDataAsync<T>(string key, out T data)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetJsonAsync(string key)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetTryJsonAsync(string key, out string data)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetDataAsync<T>(string key, T data, TimeSpan expiry)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetJsonAsync(string key, string dataJson, TimeSpan expiry)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasKeyAsync(string key)
    {
        throw new NotImplementedException();
    }
}