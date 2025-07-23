using MosqueApi.Extensions;
using WebCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureDefault();
builder.Services
    .AddConfig(builder.Configuration)
    .AddService()
    .AddInfrastructure();

var app = builder.Build();
await app.ConfigureDefault();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();