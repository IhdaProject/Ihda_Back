namespace Entity.DataTransferObjects.StaticFiles;

public record StaticFileDto(
    long Id,
    string Url,
    string Name);