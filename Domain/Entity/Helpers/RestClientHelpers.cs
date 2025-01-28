using System.Text;
using Entity.Extensions;
using RestSharp;

namespace Entity.Helpers;

public static class RestClientHelpers
{
    public static RestClient GetInstance(string? baseAddress = null)
    {
        var httpClient = new RestClient(baseAddress);
        return httpClient;
    }

    public static RestClient GetInstanceWithBaseAuth(string username, string password, string? baseAddress = null)
    {
        var restClient = RestClientHelpers.GetInstance(baseAddress);
        restClient.AddDefaultHeader("Authorization",
            "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));

        return restClient;
    }

    public static RestClient GetInstanceWithJwtAuth(string token, string? baseAddress = null)
    {
        var restClient = RestClientHelpers.GetInstance(baseAddress);
        restClient.AddDefaultHeader("Authorization", $"Bearer {token}");
        return restClient;
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
    public static async Task<TResult?> PostJsonAsync<TIn, TResult>(this RestClient client, string url, TIn data)
    {
        var content = new RestRequest(SerializerHelper.ToJsonString(data));
        var result = await client.PostAsync(content);
        
        if (result.IsSuccessful)
            throw new ApplicationException("Request not success");

        return SerializerHelper.FromJsonString<TResult>(result.Content);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="queryParams"></param>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns>
    /// </returns>
    /// <exception cref="HttpRequestException">If status code is not OK</exception>
    public static async Task<TResult?> GetJsonAsync<TResult>(this RestClient client,
        string url,
        params KeyValuePair<string, object>[]? queryParams)
    {
        var queryString = "";
        if (queryParams is not null && queryParams.Length != 0)
            queryString = "?" + string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));

        var result = await client.GetAsync(new RestRequest(url + queryString));

        if (result.IsSuccessful)
            throw new ApplicationException("Request not success");

        return SerializerHelper.FromJsonString<TResult>(result.Content);
    }
}