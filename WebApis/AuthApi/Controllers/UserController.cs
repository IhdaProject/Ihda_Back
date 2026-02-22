using AuthService.Services;
using Entity.DataTransferObjects.Authentication;
using Entity.DataTransferObjects.Users;
using Entity.Enums;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using WebCore.Attributes;
using WebCore.Controllers;

namespace AuthApi.Controllers;

public class UserController(IUserService userService) : ApiControllerBase
{
    [HttpGet]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.ViewUsers)]
    public async Task<ResponseModel<List<UserDto>>> GetUsers([FromQuery]MetaQueryModel metaQueryModel)
        => await userService.GetUsersAsync(metaQueryModel);
    [HttpGet("{id}")]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.ViewUser)]
    public async Task<ResponseModel<UserFullDto>> GetUser([FromRoute]long id)
        => await userService.GetUserByIdAsync(id);
    [HttpGet]
    [ApiGroup("Client")]
    [PermissionAuthorize(UserPermissions.ViewProfile)]
    public async Task<ResponseModel<UserFullDto>> GetProfile()
        => await userService.GetUserByIdAsync(UserId);
    [HttpPut]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.AddUserStructure)]
    public async Task<ResponseModel<bool>> AddStructure([FromBody]ChangeUserStructureDto changeUserStructure)
        => await userService.AddStructureAsync(changeUserStructure);
    [HttpDelete]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.RemoveUserStructure)]
    public async Task<ResponseModel<bool>> RemoveStructure([FromBody]ChangeUserStructureDto changeUserStructure)
        => await userService.RemoveStructureAsync(changeUserStructure);
}