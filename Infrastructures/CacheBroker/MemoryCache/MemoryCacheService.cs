using CacheBroker.Interfaces;

namespace CacheBroker.MemoryCache;

public class MemoryCacheService/*(IMemoryCache memoryCache) */: ICacheService
{
    public Task<T> GetDataAsync<T>(string key)
    {
        throw new NotImplementedException();
        /*if(memoryCache.TryGetValue<T>(key, out T data) && data != null)
            return Task.FromResult(data);
        
        throw new KeyNotFoundException();*/
    }

    public Task<bool> GetTryDataAsync<T>(string key, out T data)
    {
        throw new NotImplementedException();
        //return Task.FromResult(memoryCache.TryGetValue<T>(key, out data));
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
        => throw new NotImplementedException(); //Task.FromResult(memoryCache.TryGetValue(key, out _));
}