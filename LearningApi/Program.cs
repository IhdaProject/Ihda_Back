using Entity.Models.ExternalAPIs;
using LearningApi.Extensions;
using WebCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:1006");
builder.ConfigureDefault();

// Add services to the container.
builder.Services.AddInfrastructure();
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