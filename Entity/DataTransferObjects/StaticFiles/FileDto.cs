using Microsoft.AspNetCore.Http;

namespace Entity.DataTransferObjects.StaticFiles;

public record FileDto(
    IFormFile file,
    string fieldName,
    string? fileName);