namespace Entity.DataTransferObjects.Files;

public record FileItemDto(
    string DbUrl,
    string TempUrl);