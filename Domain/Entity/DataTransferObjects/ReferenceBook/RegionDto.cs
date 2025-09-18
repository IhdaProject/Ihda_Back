namespace Entity.DataTransferObjects.ReferenceBook;

public record RegionDto(
    long Id,
    string Name,
    int Code,
    long CountryId,
    string? CountryName) : BaseDto<long>(Id);