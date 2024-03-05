namespace WebCore.CronSchedulers;

public interface ICronJob
{
    Task Run(CancellationToken token = default);
}