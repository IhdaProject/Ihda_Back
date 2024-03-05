namespace Entity.DataTransferObjects.Authentication;

public record UserRegisterDto(
    string firstname,
    string lastname,
    string? middlename,
    string username,
    string password
);