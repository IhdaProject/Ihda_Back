using Entity.Enums;

namespace Entity.DataTransferObjects.Authentication;

public record UserRegisterDto(
    string FullName,
    DateOnly BirthDate,
    Gender Gender,
    string Pinfl,
    string UserName,
    string Password,
    string PhotoUrl);