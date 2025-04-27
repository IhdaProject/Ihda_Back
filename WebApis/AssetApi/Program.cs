using AssetApi.Extensions;
using Microsoft.Net.Http.Headers;
using WebCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureDefault();
builder.Services
    .AddService()
    .AddInfrastructure();

var app = builder.Build();
await app.ConfigureDefault();
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = (context) =>
    {
        var fileName = "Download"+Path.GetExtension(context.File.Name);
        var header = new ContentRangeHeaderValue(context.File.Length);
        context.Context.Response.Headers.ContentDisposition = $"attachment; filename=\"{fileName}\"";
        context.Context.Response.Headers.ContentType = "application/octet-stream";
        context.Context.Response.Headers[HeaderNames.CacheControl] = $"public, max-age={1 * 24 * 60 * 60}";
    },
    RequestPath = new PathString("/api"),
});

app.UseAuthorization();
app.MapControllers();
app.Run();