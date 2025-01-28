using System.IdentityModel.Tokens.Jwt;
using Entity.Models;

namespace AuthenticationBroker.TokenHandler;

public interface IJwtTokenHandler
{
    public (string refreshToken, DateTime expireDate) GenerateRefreshToken();
    JwtSecurityToken GenerateAccessToken(User user);
}