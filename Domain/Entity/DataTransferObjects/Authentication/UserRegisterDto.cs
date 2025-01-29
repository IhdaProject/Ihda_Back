namespace Entity.DataTransferObjects.Authentication;

public record UserRegisterDto(
    string FirstName,
    string LastName,
    string? MiddleName,
    string UserName,
    string Password
);