namespace Entity.DataTransferObjects.ReferenceBook;

public record ChangeTypeDto(
    TypeSchemaDto TypeSchema,
    string Token,
    string Commit);