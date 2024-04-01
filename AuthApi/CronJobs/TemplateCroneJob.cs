using AuthService.Services;
using WebCore.CronSchedulers;

namespace AuthApi.CronJobs;

public class TemplateCroneJob : ICronJob
{
    private readonly ILogger<TemplateCroneJob> _logger;
    private readonly IServiceProvider _serviceProvider;

    public TemplateCroneJob(ILogger<TemplateCroneJob> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task Run(CancellationToken token = default)
    {
        _logger.Log(LogLevel.Information,"Start template cron");
        using var scope = _serviceProvider.CreateScope();
        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
        /*authService.Register(null);*/
        _logger.Log(LogLevel.Information,"Finish template cron");
        return Task.CompletedTask;
    }
}