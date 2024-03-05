using Entity.DataTransferObjects;
using Entity.Exeptions;
using Entity.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json;
using DatabaseBroker.Repositories.Auth;
using Entity.DataTransferObjects.Authentication;
using Entity.Exeptions.Eimzo;
using Entity.Models.ExternalAPIs;
using Microsoft.EntityFrameworkCore;

namespace UserService.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _usermapper;
    private readonly IStructureRepository _structureRepository;

    public UserService(
        IUserRepository userRepository,
        IUserMapper usermapper,
        IStructureRepository structureRepository
    )
    {
        this._userRepository = userRepository;
        this._usermapper = usermapper;
        _structureRepository = structureRepository;
    }

    public async ValueTask<UserDTO> CreateUserAsync(UserForCreationDTO userForCreationDto)
    {
        ValidateUserForCreation(userForCreationDto);

        var newUser = _usermapper.MapToUser(userForCreationDto);
        // var passwordHash = _passwordHasher.Encrypt(userForCreationDto.password);
        // newUser.PasswordHash = passwordHash;
        newUser = await _userRepository.AddAsync(newUser);

        return _usermapper.MapToUserDto(newUser);
    }

    public async ValueTask<UserDTO> ModifyUserAsync(UserForModificationDTO userForModificationDto)
    {
        ValidationUserForMadification(userForModificationDto);

        var user = await _userRepository.GetByIdAsync(userForModificationDto.id);
        _usermapper.MapToUser(user, userForModificationDto);

        var newUser = await _userRepository.UpdateAsync(user);

        return _usermapper.MapToUserDto(user);
    }

    public async ValueTask<UserDTO> RemoveUserAsync(long userId)
    {
        var newUser = await _userRepository.GetByIdAsync(userId);

        ValidateUser(newUser, userId);

        var user = await _userRepository.RemoveAsync(newUser);

        return _usermapper.MapToUserDto(user);
    }

    public async ValueTask<UserDTO> CreateUserByESIAsync(UserForCreationByESIDTO userForCreationByEsiDto)
    {
        var user = new User()
        {
            FirstName = userForCreationByEsiDto.firstName,
            LastName = userForCreationByEsiDto.lastName,
            MiddleName = userForCreationByEsiDto.middleName,
            StructureId = userForCreationByEsiDto.stuructureId
        };

        var httpClient = new HttpClient();
        var url = ExternalAPI.EImzo + "Eimzo/GetVerifyPkcs";
        var data = new { Signature = userForCreationByEsiDto.signature };
        var content =
            new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new EImzoApiException(
                $"Eimzo api {response.StatusCode} response by signature: {userForCreationByEsiDto.signature}");
        }

        var result = await response.Content.ReadAsStringAsync();

        var verify = result.Substring(result.IndexOf("success") + 9, 4);

        if (verify != "true")
        {
            throw new SignatureNotValidException($"Not valid signature {userForCreationByEsiDto.signature}");
        }

        user = await _userRepository.AddAsync(user);

        // return new UserDTO(
        //     user.Id,
        //     user.UserName,
        //     user.FirstName,
        //     user.MiddleName,
        //     user.LastName,
        //     user.INN,
        //     user.StructureId);
        return null;
    }

    public async ValueTask<bool> ChangeUserStructure(ChangeUserStructureDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);

        NotFoundException.ThrowIfNull(user);

        var structure = await _structureRepository.GetByIdAsync(dto.StructureId);

        NotFoundException.ThrowIfNull(structure);

        user.StructureId = structure.Id;

        await _userRepository.UpdateAsync(user);

        return true;
    }

    public async ValueTask<User> RetrieveUserByIdAsync(long userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        ValidateUser(user, userId);

        return user;
    }

    public async Task<IEnumerable<User>> RetrieveUsersAsync()
    {
        return (await _userRepository.OrderBy(x => x.Id).ToListAsync());
    }

    public void ValidateUser(User user, long userId)
    {
        if (user is null)
        {
            throw new NotFoundException($"UserId not found: {userId}");
        }
    }

    public void ValidateUserForCreation(UserForCreationDTO userForCreationDTO)
    {
        if (userForCreationDTO.userName is null)
        {
            throw new ValidationException("userName can not be null");
        }

        if (userForCreationDTO.firstName is null)
        {
            throw new ValidationException("firstname cannot be null");
        }

        if (userForCreationDTO.inn is null)
        {
            throw new ValidationException("INN cannot be null");
        }

        if (userForCreationDTO.password is null)
        {
            throw new ValidationException("Password cannot be null");
        }
    }

    public void ValidationUserForMadification(UserForModificationDTO userForModificationDTO)
    {
        if (userForModificationDTO is null)
        {
            throw new ValidationException("can not be null");
        }
    }
}