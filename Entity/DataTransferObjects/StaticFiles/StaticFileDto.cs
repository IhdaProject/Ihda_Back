namespace Entity.DataTransferObjects.StaticFiles;

public record StaticFileDto(
    long id,
    string url,
    string name);