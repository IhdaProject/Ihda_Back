using Entity.DataTransferObjects.Authentication;

namespace AuthService.Services;

public interface IAuthService
{
    TokenDto DeleteToken(string accessToken);
    Task<bool> Register(UserRegisterDto userRegisterDto);
    Task<TokenDto> SignByPassword(AuthenticationDto authenticationDto);
    Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto);
    Task<bool> DeleteTokenAsync(TokenDto tokenDto);
}