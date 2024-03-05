using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using WebCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureDefault();

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

builder.Services.AddOcelot(builder.Configuration);

builder.WebHost.UseUrls("http://*:1000");
var app = builder.Build();

app.UseHttpsRedirection();
app.UseOcelot().Wait();

app.Run();