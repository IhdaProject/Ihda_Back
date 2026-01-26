using Entity.Enums;

namespace Entity.DataTransferObjects.ReferenceBook;

public record FieldSchemaDto(
    string Name,
    TypeForDynamic Type,
    int? TypeId ,
    bool Required ,
    bool Unique ,
    bool IsArray,
    int? Min,
    int? Max,
    int? MinLength,
    int? MaxLength,
    string? Pattern);