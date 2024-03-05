using Entity.DataTransferObjects;
using Entity.Enum;
using Entity.Models;

namespace UserService.Service;

public class UserMapper : IUserMapper
{
    public UserMapper()
    {
    }

    public User MapToUser(UserForCreationDTO userForCreationDto)
    {
        return new User
        {
            FirstName = userForCreationDto.firstName,
            MiddleName = userForCreationDto.midleName,
            LastName = userForCreationDto.lastName,
            StructureId = userForCreationDto.stuructureId
            // ESIHash = userForCreationDto.esi,
        };
    }

    public void MapToUser(User storageUser, UserForModificationDTO userForCreationDto)
    {
        storageUser.FirstName = userForCreationDto.firstname ?? storageUser.FirstName;
        storageUser.LastName = userForCreationDto.lastname ?? storageUser.LastName;
        storageUser.MiddleName = userForCreationDto.midlename ?? storageUser.MiddleName;
        // storageUser.ESIHash = userForCreationDto.esi ?? storageUser.ESIHash;
        // storageUser.Password = userForCreationDto.password ?? storageUser.Password;
    }

    public UserDTO MapToUserDto(User user)
    {
        return new UserDTO(
            user.Id,
            user.SignMethods.ToList(),
            user.FirstName,
            user.MiddleName,
            user.LastName,
            user.StructureId
            );
    }
}