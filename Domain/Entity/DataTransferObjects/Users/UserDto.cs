namespace Entity.DataTransferObjects.Users;

public record UserDto(
    long Id,
    string FullName,
    List<long> StructuresId);