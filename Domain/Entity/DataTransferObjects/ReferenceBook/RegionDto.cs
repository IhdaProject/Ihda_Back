namespace Entity.DataTransferObjects.ReferenceBook;

public record RegionDto(
    long Id,
    string Name,
    int Code,
    long CountryId) : BaseDto<long>(Id);