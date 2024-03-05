using NCrontab;

namespace WebCore.Models;

public sealed record CronRegistryEntry(Type Type, CrontabSchedule CrontabSchedule);