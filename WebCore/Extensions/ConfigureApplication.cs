using System.Text;
using System.Text.Json.Serialization;
using AuthenticationBroker.Options;
using DatabaseBroker.DataContext;
using Entity.Enum;
using Entity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using WebCore.Filters;
using WebCore.Middlewares;
using WebCore.Models;

namespace WebCore.Extensions;

public static class ConfigureApplication
{
    public static void ConfigureDefault(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<TelegramBotCredential>(builder.Configuration
            .GetSection("TelegramBotCredential"));
        
        Log.Logger = new LoggerConfiguration()
            .WriteTo
            .Console()
            .WriteTo
            .Api(builder.Services.BuildServiceProvider().GetService<IOptions<TelegramBotCredential>>(),LogEventLevel.Fatal)
            .WriteTo
            .File("logs/log-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.Logging.ClearProviders().AddSerilog();

        builder
            .Configuration
            .AddEnvironmentVariables()
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);

        builder.Services.Configure<JwtOption>(builder.Configuration
            .GetSection("JwtOption"));

        builder
            .Services
            .AddDbContextPool<ProgramDataContext>(optionsBuilder =>
            {
                optionsBuilder
                    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                optionsBuilder.UseLoggerFactory(new SerilogLoggerFactory(Log.Logger));
                optionsBuilder
                    .UseLazyLoadingProxies();
            });

        builder
            .Services
            .AddHealthChecks();

        builder
            .Services
            .AddCors(options =>
            {
                options
                    .AddDefaultPolicy(builder =>
                    {
                        builder
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
                .SwaggerDoc("Client",new OpenApiInfo(){Title = AppDomain.CurrentDomain.FriendlyName});
            
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
                        new List<string>() { }
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionsSection["SecretKey"])),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });


        builder.Services.AddScoped<GlobalExceptionHandlerMiddleware>();
    }

    public static async Task ConfigureDefault(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await using var dataContext = scope.ServiceProvider.GetService<ProgramDataContext>();
        Log.Information("{0}", "Migrations applying...");
        await dataContext?.Database.MigrateAsync()!;
        Log.Information("{0}", "Migrations applied.");
        scope.Dispose();


        app.UseCors();
        app.UseHealthChecks("/healths");

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        await app.SynchronizePermissions();
    }

    private static async Task SynchronizePermissions(this WebApplication app)
    {
        try
        {
            Log.Information("Permissions synchronization starting....");
            using var scope = app.Services.CreateScope();
            await using var dataContext = scope.ServiceProvider.GetService<ProgramDataContext>();

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
                continue;
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

            Log.Information("Permissions synchronization finished successfully.");
        }
        catch (Exception e)
        {
            Log.Error(e, "Permissions synchronization crashed.");
        }
    }
}