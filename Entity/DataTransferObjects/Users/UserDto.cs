namespace Entity.DataTransferObjects.Users;

public record UserDto(
    long Id,
    string FirstName,
    string MiddleName,
    string LastName,
    long? StructureId
);