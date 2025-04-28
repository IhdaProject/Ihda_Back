using AuthService.Services;
using Entity.DataTransferObjects.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace AuthApi.Controllers;
[ApiExplorerSettings(GroupName = "Client")]
public class AuthController(IAuthService authService) : ApiControllerBase
{
    [HttpPost]
    public async Task<ResponseModel<bool>> Register([FromBody] UserRegisterDto userRegisterDto)
        => ResponseModel<bool>.ResultFromContent(await authService.Register(userRegisterDto));
    
    [HttpPost]
    public async Task<ResponseModel<TokenDto>> Sign([FromBody] AuthenticationDto authenticationDto)
        => ResponseModel<TokenDto>.ResultFromContent(await authService.SignByPassword(authenticationDto));
    
    [HttpPost]
    public async Task<ResponseModel<TokenDto>> RefreshToken([FromBody]TokenDto tokenDto)
        => ResponseModel<TokenDto>.ResultFromContent(await authService.RefreshTokenAsync(tokenDto));
    
    [HttpDelete]
    public async Task<ResponseModel<bool>> LogOut([FromBody]TokenDto tokenDto)
        => ResponseModel<bool>.ResultFromContent(await authService.DeleteTokenAsync(tokenDto));
}