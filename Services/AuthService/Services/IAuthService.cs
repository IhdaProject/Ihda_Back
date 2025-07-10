using Entity.DataTransferObjects.Authentication;

namespace AuthService.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(UserRegisterDto userRegisterDto);
    Task<TokenDto> SignByPasswordAsync(AuthenticationDto authenticationDto);
    Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto);
    Task<bool> DeleteTokenAsync(string jti);
    Task<List<int>> GetUserPermissionsAsync(long userId);
}