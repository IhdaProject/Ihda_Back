using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthenticationBroker.Options;
using Entity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationBroker.TokenHandler;

public class JwtTokenHandler : IJwtTokenHandler
{
    private readonly JwtOption _jwtOption;

    public JwtTokenHandler(IOptions<JwtOption> option)
    {
        this._jwtOption = option.Value;
    }

    public (string refreshToken, DateTime expireDate) GenerateRefreshToken()
    {
        var bytes = new byte[64];

        using var randomGenerator =
            RandomNumberGenerator.Create();

        randomGenerator.GetBytes(bytes);
        return (Convert.ToBase64String(bytes), DateTime.UtcNow.AddMinutes(_jwtOption.ExpirationRefreshTokenInMinutes));
    }

    public JwtSecurityToken GenerateAccessToken(User user)
    {
        var claims = this.GetClaims(user);
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this._jwtOption.SecretKey));

        var token = new JwtSecurityToken(
            issuer: this._jwtOption.Issuer,
            audience: this._jwtOption.Audience,
            expires: DateTime.UtcNow.AddMinutes(this._jwtOption.ExpirationInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(
                key: authSigningKey,
                algorithm: SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(CustomClaimNames.Structure, user.StructureId?.ToString() ?? ""),
            new Claim(CustomClaimNames.RoleName, user.Structure?.Name.ToString()),
            new Claim(CustomClaimNames.UserId, user.Id.ToString()),
            new Claim(CustomClaimNames.Permissions,
                string.Join(", ", user.Structure?.StructurePermissions.Select(x => x.Permission.Code) ?? Array.Empty<int>()))
        };

        return claims;
    }
}