using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthenticationBroker.Options;
using Entity.Enums;
using Entity.Models;
using Entity.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationBroker.TokenHandler;

public class JwtTokenHandler(IOptions<JwtOption> option) : IJwtTokenHandler
{
    private readonly JwtOption _jwtOption = option.Value;
    public (string refreshToken, DateTime expireDate) GenerateRefreshToken()
    {
        var bytes = new byte[64];

        using var randomGenerator =
            RandomNumberGenerator.Create();

        randomGenerator.GetBytes(bytes);
        return (Convert.ToBase64String(bytes), DateTime.UtcNow.AddMinutes(_jwtOption.ExpirationRefreshTokenInMinutes));
    }

    public (JwtSecurityToken token,string jti,DateTime expireDate) GenerateAccessToken(User user)
    {
        var claims = GetClaims(user);
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtOption.SecretKey));

        var expire = DateTime.UtcNow.AddMinutes(_jwtOption.ExpirationInMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtOption.Issuer,
            audience: _jwtOption.Audience,
            expires: expire,
            claims: claims,
            signingCredentials: new SigningCredentials(
                key: authSigningKey,
                algorithm: SecurityAlgorithms.HmacSha256)
        );

        return (token, claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)!.Value, expire);
    }

    private static List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new (CustomClaimNames.Structures, string.Join(',',user.Structures.Select(s => s.StructureId).ToList())),
            new (CustomClaimNames.UserId, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return claims;
    }
}