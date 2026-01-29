using Entity.Models.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace MinIoBroker.Services;

public class MinioService(IOptions<MinIoConfiguration> configuration) : IMinioService
{
    private readonly string bucketName = configuration.Value.BucketName;
    private readonly IMinioClient client = new MinioClient()
        .WithEndpoint(configuration.Value.Endpoint)
        .WithCredentials(configuration.Value.AccessKey, configuration.Value.SecretKey)
        .WithSSL(configuration.Value.UseSSL)
        .Build();
    
    public async Task EnsureBucketExistsAsync()
    {
        var exists = await client.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        if (!exists)
            await client.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
    }
    public async Task UploadFileAsync(string objectName, string filePath)
    {
        await EnsureBucketExistsAsync();
        await client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithFileName(filePath));
    }
    public async Task<string> UploadFileFromFormFileAsync(IFormFile file, string objectName)
    {
        var filePath = $"{DateTime.Today:yyyy/MM/dd}";
        objectName = $"{filePath}/{objectName}";
        await EnsureBucketExistsAsync();
        await using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        ms.Position = 0;

        await client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithStreamData(ms)
            .WithObjectSize(ms.Length));
        
        return objectName;
    }
    public async Task UploadFileFromBase64Async(string objectName, string base64Data)
    {
        await EnsureBucketExistsAsync();
        var bytes = Convert.FromBase64String(base64Data);
        using var ms = new MemoryStream(bytes);
        await client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithStreamData(ms)
            .WithObjectSize(ms.Length));
    }
    public async Task UpdateFileAsync(string objectName, string filePath)
    {
        await UploadFileAsync(objectName, filePath);
    }
    public async Task DeleteFileAsync(string objectName)
    {
        await client.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName));
    }
    public async Task<string> GetPresignedUrlAsync(string objectName, int expirySeconds = 3600)
    {
        await EnsureBucketExistsAsync();
        var url = await client.PresignedGetObjectAsync(new PresignedGetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithExpiry(expirySeconds));

        var uri = new Uri(url);
        
        var relativeUrl = "/api-cloud" + uri.PathAndQuery;

        return relativeUrl;
    }
    public async Task<string> GetFileAsBase64Async(string objectName)
    {
        await EnsureBucketExistsAsync();
        using var ms = new MemoryStream();
        await client.GetObjectAsync(new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithCallbackStream(s => s.CopyTo(ms)));
        return Convert.ToBase64String(ms.ToArray());
    }
}