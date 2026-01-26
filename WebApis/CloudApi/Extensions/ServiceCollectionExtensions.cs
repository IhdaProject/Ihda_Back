using FileService.Services;

namespace CloudApi.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddConfig(IConfiguration configuration)
        {
            return services;
        }

        public IServiceCollection AddService()
        {
            services.AddScoped<IFileService, FileService.Services.FileService>();
            return services;
        }

        public IServiceCollection AddInfrastructure()
        {
            return services;
        }
    }
}