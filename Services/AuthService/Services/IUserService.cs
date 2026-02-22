using Entity.DataTransferObjects.Authentication;
using Entity.DataTransferObjects.Users;
using Entity.Models.ApiModels;

namespace AuthService.Services;

public interface IUserService
{
    Task<ResponseModel<List<UserDto>>> GetUsersAsync(MetaQueryModel metaQueryModel);
    Task<ResponseModel<UserFullDto>> GetUserByIdAsync(long userId);
    Task<ResponseModel<bool>> AddStructureAsync(ChangeUserStructureDto userStructure);
    Task<ResponseModel<bool>> RemoveStructureAsync(ChangeUserStructureDto userStructure);
}