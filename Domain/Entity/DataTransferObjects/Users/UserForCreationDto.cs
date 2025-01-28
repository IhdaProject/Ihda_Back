namespace Entity.DataTransferObjects.Users;

public record UserForCreationDto(
    string UserName,
    string Password,
    string FirstName,
    string LastName,
    string MiddleName,
    long StuructureId);