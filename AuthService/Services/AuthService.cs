using System.IdentityModel.Tokens.Jwt;
using AuthenticationBroker.TokenHandler;
using DatabaseBroker.Repositories.Auth;
using Entity.DataTransferObjects.Authentication;
using Entity.Enum;
using Entity.Exeptions;
using Entity.Extensions;
using Entity.Models;
using Entity.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthService.Services;

public class AuthService(
    IJwtTokenHandler jwtTokenHandler,
    IUserRepository userRepository,
    ITokenRepository tokenRepository,
    ISignMethodsRepository signMethodsRepository,
    IStructureRepository structureRepository)
    : IAuthService
{

    public async Task<bool> Register(UserRegisterDto userRegisterDto)
    {
        var hasStoredUser = await signMethodsRepository.OfType<DefaultSignMethod>()
            .AnyAsync(x => x.Username == userRegisterDto.username);
        if (hasStoredUser)
            throw new AlreadyExistsException("User name or login already exists");

        User newUser = new User()
        {
            FirstName = userRegisterDto.firstname,
            LastName = userRegisterDto.lastname,
            MiddleName = userRegisterDto.middlename,
            SignMethods = new List<SignMethod>(),
            StructureId = (await structureRepository.FirstOrDefaultAsync(x => x.IsDefault))?.Id,
        };

        var storedUser = await userRepository.AddAsync(newUser);
        await signMethodsRepository.AddAsync(new DefaultSignMethod()
        {
            Username = userRegisterDto.username,
            PasswordHash = PasswordHelper.Encrypt(userRegisterDto.password),
            UserId = storedUser.Id
        });
        return true;
    }

    public async Task<TokenDto> SignByPassword(AuthenticationDto authenticationDto)
    {
        var signMethod = await signMethodsRepository.OfType<DefaultSignMethod>()
            .FirstOrDefaultAsync(x => x.Username == authenticationDto.username);

        if (signMethod is null)
            throw new NotFoundException("That credentials not found");

        if (!PasswordHelper.Verify(signMethod.PasswordHash, authenticationDto.password))
            throw new NotFoundException("User not found");

        var user = signMethod.User;

        var refreshToken = jwtTokenHandler.GenerateRefreshToken();

        var token = new TokenModel()
        {
            UserId = user.Id,
            TokenType = TokenTypes.Normal,
            AccessToken = new JwtSecurityTokenHandler()
                .WriteToken(jwtTokenHandler.GenerateAccessToken(user)),
            RefreshToken = refreshToken.refreshToken,
            ExpireRefreshToken = refreshToken.expireDate
        };

        token = await tokenRepository.AddAsync(token);

        var tokenDto = new TokenDto(
            token.AccessToken,
            token.RefreshToken,
            token.ExpireRefreshToken);

        return tokenDto;
    }

    public async Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto)
    {
        var token = await tokenRepository
            .GetAllAsQueryable()
            .FirstOrDefaultAsync(t => t.AccessToken == tokenDto.accessToken
                && t.RefreshToken == tokenDto.refreshToken)
            ?? throw new NotFoundException("Not Found Token");

        if (token.ExpireRefreshToken < DateTime.UtcNow)
        {
            var deleteToken = await tokenRepository.RemoveAsync(token);
            throw new AlreadyExistsException("Refresh token timed out");
        }

        token.User = await userRepository.GetByIdAsync(token.UserId);

        token.AccessToken = new JwtSecurityTokenHandler()
            .WriteToken(jwtTokenHandler.GenerateAccessToken(token.User));
        token.UpdatedAt = DateTime.UtcNow;

        token = await tokenRepository.UpdateAsync(token);

        var newTokenDto = new TokenDto(
            token.AccessToken,
            token.RefreshToken,
            token.ExpireRefreshToken);

        return newTokenDto;
    }

    public async Task<bool> DeleteTokenAsync(TokenDto tokenDto)
    {
        var token = await tokenRepository
                        .GetAllAsQueryable()
                        .FirstOrDefaultAsync(t => t.AccessToken == tokenDto.accessToken
                                                  && t.RefreshToken == tokenDto.refreshToken)
                    ?? throw new NotFoundException("Not Found Token");

        var deleteToken = await tokenRepository.RemoveAsync(token);

        return deleteToken.Id == token.Id;
    }
}