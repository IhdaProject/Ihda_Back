using AuthenticationBroker.TokenHandler;
using CacheBroker.Interfaces;
using CacheBroker.MemoryCache;
using DatabaseBroker.Repositories.Auth;
using DatabaseBroker.Repositories.Mosques;
using MosqueService.Services;

namespace MosqueApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<IMosqueService, MosqueService.Services.MosqueService>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IMosqueRepository, MosqueRepository>();
        services.AddScoped<IMosquePrayerTimeRepository, MosquePrayerTimeRepository>();

        return services;
    }
}