namespace Entity.DataTransferObjects.ReferenceBook;

public record ChangeTypeDto(
    TypeSchemaDto TypeSchema,
    string Commit,
    string Token);