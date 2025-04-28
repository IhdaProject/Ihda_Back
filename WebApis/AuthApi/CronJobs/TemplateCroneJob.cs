using AuthService.Services;
using Serilog;
using WebCore.CronSchedulers;

namespace AuthApi.CronJobs;

public class TemplateCroneJob(IServiceProvider serviceProvider) : ICronJob
{
    public Task Run(CancellationToken token = default)
    {
        Log.Information("Start template cron");
        using var scope = serviceProvider.CreateScope();
        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
        /*authService.Register(null);*/
        Log.Information("Finish template cron");
        return Task.CompletedTask;
    }
}