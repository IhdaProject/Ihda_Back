using AuthApi.Extensions;
using WebCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:1001");
builder.ConfigureDefault();
builder.Services.AddConfig(builder.Configuration);
// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddService();


var app = builder.Build();
await app.ConfigureDefault();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();