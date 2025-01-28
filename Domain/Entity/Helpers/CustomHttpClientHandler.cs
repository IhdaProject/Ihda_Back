using System.Diagnostics;
using Serilog;

namespace Entity.Helpers;

public class CustomHttpClientHandler : HttpClientHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            Log.Information("Request started: {0} URL: {1}",
                request.Method.Method,
                request.RequestUri?.ToString());
            return base.SendAsync(request, cancellationToken);
        }
        finally
        {
            stopwatch.Stop();
            Log.Information("Request finished: {0} URL: {1} - {2} s.",
                request.Method.Method,
                request.RequestUri?.ToString(),
                stopwatch.Elapsed.TotalSeconds
            );
        }
    }
}