namespace CacheBroker.Interfaces;

public interface ICacheService
{
    Task<T> GetDataAsync<T>(string key);
    Task<bool> GetTryDataAsync<T>(string key, out T data);
    Task<string> GetJsonAsync(string key);
    Task<bool> GetTryJsonAsync(string key, out string data);
    Task<bool> SetDataAsync<T>(string key, T data, TimeSpan expiry);
    Task<bool> SetJsonAsync(string key, string dataJson, TimeSpan expiry);
    Task<bool> HasKeyAsync(string key);
}