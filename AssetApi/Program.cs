using AssetApi.Extensions;
using Microsoft.Net.Http.Headers;
using WebCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:1003");
builder.ConfigureDefault();
// Add services to the container.
builder.Services.AddService();
builder.Services.AddInfrastructure();
builder.Services.AddService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDirectoryBrowser();
var app = builder.Build();
await app.ConfigureDefault();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = (context) =>
    {
        var fileName = "Download(BusinessStudy)"+Path.GetExtension(context.File.Name);
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