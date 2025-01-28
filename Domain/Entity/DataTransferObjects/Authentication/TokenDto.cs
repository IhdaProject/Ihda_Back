using Entity.Models;

namespace Entity.DataTransferObjects.Authentication;

public record TokenDto(
    string accessToken,
    string refreshToken,
    DateTime? expireRefreshToken);