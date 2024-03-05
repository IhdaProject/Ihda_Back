using AuthService.Services;
using Entity.DataTransferObjects.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace AuthApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    public async Task<ResponseModel> Sign(
        [FromBody] AuthenticationDto authenticationDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.SignByPassword(authenticationDto));
    }

    [HttpPost]
    public async Task<ResponseModel> Register(
        [FromBody] UserRegisterDto userRegisterDto)
    {
        return (await _authService
            .Register(userRegisterDto), 
            StatusCodes.Status200OK);
    }
    
    [HttpPost]
    public async Task<ResponseModel> RefreshToken([FromBody]TokenDto tokenDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.RefreshTokenAsync(tokenDto));
    }
    
    [HttpDelete]
    public async Task<ResponseModel> LogOut([FromBody]TokenDto tokenDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _authService.DeleteTokenAsync(tokenDto));
    }
}