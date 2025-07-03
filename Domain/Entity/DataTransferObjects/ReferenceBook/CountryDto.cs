namespace Entity.DataTransferObjects.ReferenceBook;

public record CountryDto(
    long Id,
    string Name,
    int Code) : BaseDto<long>(Id);