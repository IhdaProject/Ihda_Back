using Microsoft.AspNetCore.Http;

namespace MinIoBroker.Services;

public interface IMinioService
{
    Task EnsureBucketExistsAsync();
    Task UploadFileAsync(string objectName, string filePath);
    Task<string> UploadFileFromFormFileAsync(IFormFile file, string objectName);
    Task UploadFileFromBase64Async(string objectName, string base64Data);
    Task UpdateFileAsync(string objectName, string filePath);
    Task DeleteFileAsync(string objectName);
    Task<string> GetPresignedUrlAsync(string objectName, int expirySeconds = 3600);
    Task<string> GetFileAsBase64Async(string objectName);
}