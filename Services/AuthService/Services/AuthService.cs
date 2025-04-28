using System.IdentityModel.Tokens.Jwt;
using AuthenticationBroker.TokenHandler;
using DatabaseBroker.Repositories.Auth;
using Entity.DataTransferObjects.Authentication;
using Entity.Enums;
using Entity.Exceptions;
using Entity.Exeptions;
using Entity.Helpers;
using Entity.Models;
using Entity.Models.Auth;
using Microsoft.EntityFrameworkCore;

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
            .AnyAsync(x => x.Username == userRegisterDto.UserName);
        if (hasStoredUser)
            throw new AlreadyExistsException("User name or login already exists");

        var newUser = new User
        {
            FullName = userRegisterDto.FullName,
            Gender = userRegisterDto.Gender,
            BirthDate = userRegisterDto.BirthDate.ToDateTime(new TimeOnly(0,0,0)),
            SignMethods = new List<SignMethod>(),
            StructureId = (await structureRepository.FirstOrDefaultAsync(x => x.IsDefault))?.Id,
        };

        var storedUser = await userRepository.AddAsync(newUser);
        var salt = Guid.NewGuid().ToString();
        await signMethodsRepository.AddAsync(new DefaultSignMethod()
        {
            Username = userRegisterDto.UserName,
            Salt = salt,
            PasswordHash = PasswordHelper.Encrypt(userRegisterDto.Password, salt),
            UserId = storedUser.Id
        });
        return true;
    }
    public async Task<TokenDto> SignByPassword(AuthenticationDto authenticationDto)
    {
        var signMethod = await signMethodsRepository.OfType<DefaultSignMethod>()
            .FirstOrDefaultAsync(x => x.Username == authenticationDto.UserName);

        if (signMethod is null)
            throw new NotFoundException("That credentials not found");

        if (!PasswordHelper.Verify(signMethod.PasswordHash, authenticationDto.Password, signMethod.Salt))
            throw new NotFoundException("User not found");

        var user = signMethod.User;

        var refreshToken = jwtTokenHandler.GenerateRefreshToken();
        var accessToken = jwtTokenHandler.GenerateAccessToken(user);

        var token = new TokenModel
        {
            UserId = user.Id,
            TokenType = TokenTypes.Normal,
            AccessTokenId = accessToken.jti,
            RefreshToken = refreshToken.refreshToken,// TODO : sha256 (id, refreshToken, expireDate)
            ExpireRefreshToken = refreshToken.expireDate
        };

        token = await tokenRepository.AddAsync(token);

        var tokenDto = new TokenDto(
            new JwtSecurityTokenHandler().WriteToken(accessToken.token),
            token.RefreshToken,
            token.ExpireRefreshToken);

        return tokenDto;
    }
    public async Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto)
    {
        var token = await tokenRepository
            .GetAllAsQueryable()
            .FirstOrDefaultAsync(t => t.AccessTokenId == tokenDto.AccessToken
                && t.RefreshToken == tokenDto.RefreshToken)
            ?? throw new NotFoundException("Not Found Token");

        if (token.ExpireRefreshToken < DateTime.UtcNow)
        {
            var deleteToken = await tokenRepository.RemoveAsync(token);
            throw new AlreadyExistsException("Refresh token timed out");
        }

        token.User = await userRepository.GetByIdAsync(token.UserId);

        token.AccessTokenId = new JwtSecurityTokenHandler()
            .WriteToken(jwtTokenHandler.GenerateAccessToken(token.User).token);
        token.UpdatedAt = DateTime.UtcNow;

        token = await tokenRepository.UpdateAsync(token);

        var newTokenDto = new TokenDto(
            token.AccessTokenId,
            token.RefreshToken,
            token.ExpireRefreshToken);

        return newTokenDto;
    }
    public async Task<bool> DeleteTokenAsync(TokenDto tokenDto)
    {
        var token = await tokenRepository
                        .GetAllAsQueryable()
                        .FirstOrDefaultAsync(t => t.AccessTokenId == tokenDto.AccessToken
                                                  && t.RefreshToken == tokenDto.RefreshToken)
                    ?? throw new NotFoundException("Not Found Token");

        var deleteToken = await tokenRepository.RemoveAsync(token);

        return deleteToken.Id == token.Id;
    }
}