using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using WebCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfigurationJson();
builder.RunPort();
builder.AddSerilog();
builder.AddUseCors();
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
app.UseHttpsRedirection();
app.UseOcelot().Wait();
app.AppStartLog();
app.Run();
