using System.Text;
using System.Text.Json;
using Serilog;

namespace WebCore.Middlewares;

public class RequestResponseLoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var requestLog = await FormatRequestAsync(context);
        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);

        var responseLog = await FormatResponseAsync(context);

        Log.Information("Request: {Request} Response: {Response}", requestLog, responseLog);

        await responseBody.CopyToAsync(originalBodyStream);
    }
    private static async Task<string> FormatRequestAsync(HttpContext context)
    {
        context.Request.EnableBuffering();

        var body = "";
        if (context.Request.ContentLength > 0 && context.Request.Body.CanRead)
        {
            context.Request.Body.Position = 0;
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        var requestInfo = new
        {
            Scheme = context.Request.Scheme,
            Host = context.Request.Host.ToString(),
            Path = context.Request.Path,
            QueryString = context.Request.QueryString.ToString(),
            Method = context.Request.Method,
            Headers = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
            Cookies = context.Request.Cookies.ToDictionary(c => c.Key, c => c.Value),
            Form = context.Request.HasFormContentType 
                ? context.Request.Form.ToDictionary(f => f.Key, f => f.Value.ToString()) 
                : null,
            Body = body
        };

        return JsonSerializer.Serialize(requestInfo, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
    private static async Task<string> FormatResponseAsync(HttpContext context)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        var responseInfo = new
        {
            StatusCode = context.Response.StatusCode,
            Headers = context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
            Body = bodyText
        };

        return JsonSerializer.Serialize(responseInfo, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}
