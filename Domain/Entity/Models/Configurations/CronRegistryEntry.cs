using NCrontab;

namespace Entity.Models.Configurations;

public sealed record CronRegistryEntry(Type Type, CrontabSchedule CrontabSchedule);