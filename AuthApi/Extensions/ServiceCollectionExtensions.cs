using AuthenticationBroker.TokenHandler;
using AuthService.Services;
using DatabaseBroker.Repositories.Auth;
using RoleService.Service;

namespace AuthApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<HttpClient, HttpClient>();
        services.AddScoped<IAuthService, AuthService.Services.AuthService>();
        services.AddScoped<IRoleService, RoleService.Service.RoleService>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStructureRepository, StructureRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IStructurePermissionRepository, StructurePermissionRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        services.AddScoped<ISignMethodsRepository, SignMethodsRepository>();


        return services;
    }
}