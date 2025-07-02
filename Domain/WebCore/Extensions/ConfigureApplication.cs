using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using AuthenticationBroker.Options;
using CacheBroker.Interfaces;
using CacheBroker.MemoryCache;
using CacheBroker.RedisCache;
using DatabaseBroker.DataContext;
using DatabaseBroker.Repositories;
using Entity.Enums;
using Entity.Models.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NCrontab;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using WebCore.Attributes;
using WebCore.Constants;
using WebCore.CronSchedulers;
using WebCore.Filters;
using WebCore.Middlewares;
using WebCore.Models;

namespace WebCore.Extensions;

public static class ConfigureApplication
{
    public static void ConfigureDefault(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile(Path.Combine(Path.Combine(Directory.GetParent(builder.Environment.ContentRootPath)!.Parent!.FullName,
                "Domain", "WebCore"), "GeneralSettings.json"), optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
        
        builder.WebHost.UseUrls(builder.Configuration.GetConnectionString("Port")!);
        
        builder.Services.Configure<TelegramBotCredential>(builder.Configuration
            .GetSection("TelegramBotCredential"));
        
        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File(
                $"Logs/mylog-.log",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: 5 * 1024 * 1024);
        
        var environment = builder.Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT");

        if (environment is null || !environment.Equals("Development", StringComparison.OrdinalIgnoreCase))
            loggerConfig = loggerConfig.Enrich.FromLogContext()
                .WriteTo.Api(
                    builder.Services.BuildServiceProvider().GetService<IOptions<TelegramBotCredential>>(),
                    LogEventLevel.Fatal);

        Log.Logger = loggerConfig.CreateLogger();

        builder
            .Configuration
            .AddEnvironmentVariables()
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);

        builder.Services.Configure<JwtOption>(builder.Configuration
            .GetSection("JwtOption"));
        
        builder
            .Services
            .AddDbContextPool<IhdaDataContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                optionsBuilder.UseLoggerFactory(new SerilogLoggerFactory(Log.Logger));
                optionsBuilder.UseLazyLoadingProxies();
            });
        
        builder.Services.AddScoped(typeof(GenericRepository<,>));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        builder.Services.AddMemoryCache();

        builder
            .Services
            .AddHealthChecks();

        builder
            .Services
            .AddCors(options =>
            {
                options
                    .AddDefaultPolicy(policyBuilder =>
                    {
                        policyBuilder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

        builder.Services
            .Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ModelValidationFilter>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options
                .SwaggerDoc("Client",new OpenApiInfo{Title = AppDomain.CurrentDomain.FriendlyName});
            
            options
                .SwaggerDoc("Admin",new OpenApiInfo{Title = AppDomain.CurrentDomain.FriendlyName});
            
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                var apiGroupAttribute = apiDesc.ActionDescriptor.EndpointMetadata
                    .OfType<ApiGroupAttribute>()
                    .FirstOrDefault();

                return apiGroupAttribute != null && apiGroupAttribute.GroupNames.Contains(docName);
            });
            
            options
                .AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

            options
                .AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>
                        {
                            Capacity = 0
                        }
                    }
                });
        });

        builder.Services
            .AddAuthorization()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptionsSection = builder.Configuration.GetSection("JwtOption");

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptionsSection["Issuer"],

                    ValidateAudience = true,
                    ValidAudience = jwtOptionsSection["Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionsSection["SecretKey"]!)),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        if(context.SecurityToken is not JwtSecurityToken securityToken) return Task.CompletedTask;

                        var tokenId = securityToken.Id;
                        var cache = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
                        
                        if(!cache.HasKeyAsync($"tokenId:{tokenId}").Result) return Task.CompletedTask;
                        
                        context.NoResult();
                        context.Response.StatusCode = 401;
                        return context.Response.WriteAsJsonAsync(new
                        {
                            message = "This token is blacklisted"
                        });
                    },
                    OnAuthenticationFailed = _ => Task.CompletedTask
                };
            });

        if (environment is null || !environment.Equals("Development", StringComparison.OrdinalIgnoreCase))
            builder.Services.AddScoped<ICacheService, RedisCacheService>();
        else 
            builder.Services.AddScoped<ICacheService, MemoryCacheService>();
        
        builder.Services.AddScoped<RequestResponseLoggingMiddleware>();
        builder.Services.AddScoped<GlobalExceptionHandlerMiddleware>();
    }

    public static async Task ConfigureDefault(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/Client/swagger.json", "Client API");
            options.SwaggerEndpoint("/swagger/Admin/swagger.json", "Admin API");
        });
        //using var scope = app.Services.CreateScope();
        //await using var dataContext = scope.ServiceProvider.GetService<IhdaDataContext>();
        Log.Information("{0}", "Migrations applying...");
        //await dataContext?.Database.MigrateAsync()!;
        Log.Information("{0}", "Migrations applied.");
        //scope.Dispose();

        app.UseHealthChecks("/healths");

        app.UseMiddleware<RequestResponseLoggingMiddleware>();
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        //await app.SynchronizePermissions();
        
        Log.Fatal("Application starting...");
    }

    private static async Task SynchronizePermissions(this WebApplication app)
    {
        try
        {
            Log.Information("Permissions synchronization starting....");
            using var scope = app.Services.CreateScope();
            await using var dataContext = scope.ServiceProvider.GetService<IhdaDataContext>();

            ArgumentNullException.ThrowIfNull(dataContext);

            var permissionCodes = typeof(UserPermissions).GetEnumValues().Cast<object>();

            var enumerable = permissionCodes as object[] ?? permissionCodes.ToArray();
            foreach (var permissionCode in enumerable)
            {
                var storedCode = await dataContext.Permissions.FirstOrDefaultAsync(x => x.Code == (int)permissionCode);
                if (storedCode is not null) continue;
                await dataContext.Permissions.AddAsync(new Permission()
                {
                    Code = (int)permissionCode,
                    Name = permissionCode.ToString() ?? string.Empty
                });
            }

            await dataContext.SaveChangesAsync();

            var codes = enumerable.Cast<int>();

            var removedCodes = await dataContext
                .Permissions
                .Where(x => codes.All(pc => pc != x.Code))
                .ToListAsync();

            if (!removedCodes.IsNullOrEmpty())
            {
                dataContext.Permissions.RemoveRange(removedCodes);
                await dataContext.SaveChangesAsync();
            }

            #region default Structure data
            if (!await dataContext.Structures.AnyAsync(s => s.IsDefault))
            {
                int[] defaultPermission = [(int)UserPermissions.LogOut];

                await dataContext.Structures.AddAsync(new Structure()
                {
                    Id = 1,
                    IsDefault = true,
                    Name = "Default",
                    StructurePermissions = dataContext.Permissions
                        .Where(p => defaultPermission.Contains(p.Code))
                        .Select(p => new StructurePermission()
                            {
                                PermissionId = p.Id
                            })
                        .ToList()
                });
            
                await dataContext.SaveChangesAsync();
            }
            #endregion
            
            StaticCache.Permissions = await dataContext.Structures
                .Select(s => new { s.Id, permissionCodes = s.StructurePermissions.Select(sp => sp.Permission.Code)})
                .ToDictionaryAsync(s => s.Id, s => s.permissionCodes.ToList());;

            Log.Information("Permissions synchronization finished successfully.");
        }
        catch (Exception e)
        {
            Log.Error(e, "Permissions synchronization crashed.");
        }
    }
    public static IServiceCollection AddCronJob<T>(this IServiceCollection services, string cronExpression)
        where T : class, ICronJob
    {
        var cron = CrontabSchedule.TryParse(cronExpression)
                   ?? throw new ArgumentException("Invalid cron expression", nameof(cronExpression));

        var entry = new CronRegistryEntry(typeof(T), cron);

        services.AddHostedService<CronScheduler>();
        services.TryAddSingleton<T>();
        services.AddSingleton(entry);

        return services;
    }
}