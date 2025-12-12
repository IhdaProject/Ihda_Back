using Microsoft.AspNetCore.Http;

namespace Entity.DataTransferObjects.Files;

public record FileDto(
    IFormFile File);