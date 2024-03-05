using Entity.DataTransferObjects;
using Entity.DataTransferObjects.Authentication;
using Entity.Models;

namespace UserService.Service;

public interface IUserService
{
    ValueTask<UserDTO> CreateUserAsync(UserForCreationDTO userForCreationDto);
    Task<IEnumerable<User>> RetrieveUsersAsync();
    ValueTask<User> RetrieveUserByIdAsync(long userId);
    ValueTask<UserDTO> ModifyUserAsync(UserForModificationDTO userForModificationDto);
    ValueTask<UserDTO> RemoveUserAsync(long userId);
    ValueTask<UserDTO> CreateUserByESIAsync(UserForCreationByESIDTO userForCreationByEsiDto);
    ValueTask<bool> ChangeUserStructure(ChangeUserStructureDto dto);
}