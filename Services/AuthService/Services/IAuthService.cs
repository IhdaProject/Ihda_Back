using Entity.DataTransferObjects.Authentication;

namespace AuthService.Services;

public interface IAuthService
{
    Task<bool> Register(UserRegisterDto userRegisterDto);
    Task<TokenDto> SignByPassword(AuthenticationDto authenticationDto);
    Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto);
    Task<bool> DeleteTokenAsync(string jti);
}