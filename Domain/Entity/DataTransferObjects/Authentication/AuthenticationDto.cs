namespace Entity.DataTransferObjects.Authentication;

public record AuthenticationDto(
    string username,
    string password);