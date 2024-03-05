namespace Entity.DataTransferObjects;

public record UserForCreationDTO(
    string userName,
    string password,
    string firstName,
    string lastName,
    string midleName,
    long stuructureId);