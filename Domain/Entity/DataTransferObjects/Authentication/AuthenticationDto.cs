namespace Entity.DataTransferObjects.Authentication;

public record AuthenticationDto(
    string UserName,
    string Password);