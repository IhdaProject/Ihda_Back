using Entity.Models;

namespace Entity.DataTransferObjects.Authentication;

public record TokenDto(
    string AccessToken,
    string RefreshToken,
    DateTime? ExpireRefreshToken);