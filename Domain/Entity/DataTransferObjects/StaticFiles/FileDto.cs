using Microsoft.AspNetCore.Http;

namespace Entity.DataTransferObjects.StaticFiles;

public record FileDto(
    IFormFile File,
    string FieldName,
    string? FileName);