using Microsoft.AspNetCore.Http;

namespace Entity.DataTransferObjects.Files;

public record FileItemDto(
    IFormFile File,
    string DbUrl,
    string TempUrl);