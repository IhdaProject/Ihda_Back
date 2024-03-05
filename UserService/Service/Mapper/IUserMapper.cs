using Entity.DataTransferObjects;
using Entity.Models;

namespace UserService.Service;

public interface IUserMapper
{
    UserDTO MapToUserDto(User user);
    User MapToUser(UserForCreationDTO userForCreationDto);
    void MapToUser(User storageUser, UserForModificationDTO userForCreationDto);
}