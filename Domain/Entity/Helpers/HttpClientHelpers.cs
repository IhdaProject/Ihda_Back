using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Entity.Extensions;

namespace Entity.Helpers;

public static class HttpClientHelpers
{
    public static HttpClient GetInstance(string? baseAddress = null)
    {
        var httpClient = new HttpClient(new CustomHttpClientHandler())
        {
            BaseAddress = new Uri(baseAddress)
        };
        return httpClient;
    }

    public static HttpClient GetInstanceWithBaseAuth(string username, string password, string? baseAddress = null)
    {
        var httpClient = GetInstance(baseAddress);
        httpClient.DefaultRequestHeaders.Add("Authorization",
            "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));

        return httpClient;
    }

    public static HttpClient GetInstanceWithJwtAuth(string token, string? baseAddress = null)
    {
        var httpClient = GetInstance(baseAddress);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        return httpClient;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns>
    /// </returns>
    /// <exception cref="HttpRequestException">If status code is not OK</exception>
    public static async Task<TResult?> PostJsonAsync<TIn, TResult>(this HttpClient client, string url, TIn data)
    {
        HttpContent content = new StringContent(SerializerHelper.ToJsonString(data));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/JSON");
        var result = await client.PostAsync(url, content);

        result.EnsureSuccessStatusCode();
        return await result.Content.ReadFromJsonAsync<TResult>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns>
    /// </returns>
    /// <exception cref="HttpRequestException">If status code is not OK</exception>
    public static async Task<TResult?> GetJsonAsync<TResult>(this HttpClient client, string url,
        params KeyValuePair<string, object>[] queryParams)
    {
        string queryString = "";
        if (queryParams is not null && queryParams.Length != 0)
            queryString = "?" + string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var result = await client.GetAsync(url + queryString);

        result.EnsureSuccessStatusCode();
        return await result.Content.ReadFromJsonAsync<TResult>();
    }
}