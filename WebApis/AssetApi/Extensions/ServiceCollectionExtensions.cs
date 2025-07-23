using DatabaseBroker.Repositories.StaticFiles;
using StaticFileService.Service;

namespace AssetApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<IStaticFileService, StaticFileService.Service.StaticFileService>();
        return services;
    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IStaticFileRepository, StaticFileRepository>();
        return services;
    }
}