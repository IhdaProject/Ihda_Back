namespace Entity.DataTransferObjects.Users;

public record UserFullDto(
    long Id,
    string FullName,
    List<long> StructuresId
);