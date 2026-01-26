namespace Entity.DataTransferObjects.ReferenceBook;

public record TypeSchemaDto(
    long Id,
    string Name,
    string Description,
    List<FieldSchemaDto> Fields)
    : BaseDto<long>(Id);