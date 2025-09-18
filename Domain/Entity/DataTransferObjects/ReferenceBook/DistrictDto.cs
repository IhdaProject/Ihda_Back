namespace Entity.DataTransferObjects.ReferenceBook;

public record DistrictDto(
    long Id,
    string Name,
    int Code,
    long RegionId,
    string? RegionName) : BaseDto<long>(Id);