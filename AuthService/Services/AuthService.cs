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

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenHandler _jwtTokenHandler;
    private readonly ITokenRepository _tokenRepository;
    private readonly IConfiguration _configuration;
    private readonly ISignMethodsRepository _signMethodsRepository;
    private readonly IStructureRepository _structureRepository;
    private readonly HttpClient _httpClient;

    public AuthService(
        IJwtTokenHandler jwtTokenHandler,
        IUserRepository userRepository,
        ITokenRepository tokenRepository,
        IConfiguration configuration,
        ISignMethodsRepository signMethodsRepository,
        IStructureRepository structureRepository,
        HttpClient httpClient)
    {
        _jwtTokenHandler = jwtTokenHandler;
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _configuration = configuration;
        _signMethodsRepository = signMethodsRepository;
        _structureRepository = structureRepository;
        _httpClient = httpClient;
    }

    public TokenDto DeleteToken(string accessToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Register(UserRegisterDto userRegisterDto)
    {
        var hasStoredUser = await _signMethodsRepository.OfType<DefaultSignMethod>()
            .AnyAsync(x => x.Username == userRegisterDto.username);
        if (hasStoredUser)
            throw new AlreadyExistsException("User name or login already exists");

        User newUser = new User()
        {
            FirstName = userRegisterDto.firstname,
            LastName = userRegisterDto.lastname,
            MiddleName = userRegisterDto.middlename,
            SignMethods = new List<SignMethod>(),
            StructureId = (await _structureRepository.FirstOrDefaultAsync(x => x.IsDefault))?.Id,
        };

        var storedUser = await _userRepository.AddAsync(newUser);
        await _signMethodsRepository.AddAsync(new DefaultSignMethod()
        {
            Username = userRegisterDto.username,
            PasswordHash = PasswordHelper.Encrypt(userRegisterDto.password),
            UserId = storedUser.Id
        });
        return true;
    }

    public async Task<TokenDto> SignByPassword(AuthenticationDto authenticationDto)
    {
        var signMethod = await _signMethodsRepository.OfType<DefaultSignMethod>()
            .FirstOrDefaultAsync(x => x.Username == authenticationDto.username);

        if (signMethod is null)
            throw new NotFoundException("That credentials not found");

        if (!PasswordHelper.Verify(signMethod.PasswordHash, authenticationDto.password))
            throw new NotFoundException("User not found");

        var user = signMethod.User;

        var refreshToken = _jwtTokenHandler.GenerateRefreshToken();

        var token = new TokenModel()
        {
            UserId = user.Id,
            TokenType = TokenTypes.Normal,
            AccessToken = new JwtSecurityTokenHandler()
                .WriteToken(_jwtTokenHandler.GenerateAccessToken(user)),
            RefreshToken = refreshToken.refreshToken,
            ExpireRefreshToken = refreshToken.expireDate
        };

        token = await _tokenRepository.AddAsync(token);

        var tokenDto = new TokenDto(
            token.AccessToken,
            token.RefreshToken,
            token.ExpireRefreshToken);

        return tokenDto;
    }

    public async Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto)
    {
        var token = await _tokenRepository
            .GetAllAsQueryable()
            .FirstOrDefaultAsync(t => t.AccessToken == tokenDto.accessToken
                && t.RefreshToken == tokenDto.refreshToken)
            ?? throw new NotFoundException("Not Found Token");

        if (token.ExpireRefreshToken < DateTime.UtcNow)
        {
            var deleteToken = await _tokenRepository.RemoveAsync(token);
            throw new AlreadyExistsException("Refresh token timed out");
        }

        token.User = await _userRepository.GetByIdAsync(token.UserId);

        token.AccessToken = new JwtSecurityTokenHandler()
            .WriteToken(_jwtTokenHandler.GenerateAccessToken(token.User));
        token.UpdatedAt = DateTime.UtcNow;

        token = await _tokenRepository.UpdateAsync(token);

        var newTokenDto = new TokenDto(
            token.AccessToken,
            token.RefreshToken,
            token.ExpireRefreshToken);

        return newTokenDto;
    }

    public async Task<bool> DeleteTokenAsync(TokenDto tokenDto)
    {
        var token = await _tokenRepository
                        .GetAllAsQueryable()
                        .FirstOrDefaultAsync(t => t.AccessToken == tokenDto.accessToken
                                                  && t.RefreshToken == tokenDto.refreshToken)
                    ?? throw new NotFoundException("Not Found Token");

        var deleteToken = await _tokenRepository.RemoveAsync(token);

        return deleteToken.Id == token.Id;
    }
}