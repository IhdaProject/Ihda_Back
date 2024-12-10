using Entity.DataTransferObjects.Role;
using Microsoft.AspNetCore.Mvc;
using RoleService.Service;
using Entity.Enum;
using Entity.Models.Auth;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;

namespace AuthApi.Controllers;
[ApiExplorerSettings(GroupName = "Client")]
public class RoleController(IRoleService structureService) : ApiControllerBase
{
    [HttpPost, PermissionAuthorize(UserPermissions.StructureCreate)]
    public async Task<ResponseModel> CreateStructure(StructureForCreationDto structureDto)
        => ResponseModel.ResultFromContent(await structureService.CreateStructureAsync(structureDto));
    
    [HttpPut, PermissionAuthorize(UserPermissions.StructureEdit)]
    public async Task<ResponseModel> PutStructure(StructureDto structure)
        => ResponseModel.ResultFromContent(await structureService.ModifyStructureAsync(structure));

    [HttpGet, PermissionAuthorize(UserPermissions.StructureView)]
    public async Task<ResponseModel> GetAllStructures()
        => ResponseModel.ResultFromContent(await structureService.RetrieveStructureAsync(""));

    [HttpPut, PermissionAuthorize(UserPermissions.PermissionNameEdit)]
    public async Task<ResponseModel<Permission>> UpdatePermissionName(Permission permissionName)
        => ResponseModel<Permission>.ResultFromContent(await structureService.ModifyPermissionAsync(permissionName));

    [HttpGet, PermissionAuthorize(UserPermissions.PermissionView)]
    public async Task<ResponseModel<List<Permission>>> GetPermissions()
        => ResponseModel<List<Permission>>.ResultFromContent(await structureService.RetrievePermissionAsync(""));

    [HttpDelete, PermissionAuthorize(UserPermissions.StructureEdit)]
    public async Task<ResponseModel<StructurePermissionDto>> DeleteStructurePermission(StructurePermissionForCreationDto structurePermission)
        => ResponseModel<StructurePermissionDto>.ResultFromContent(await structureService.RemoveStructurePermissionAsync(structurePermission));
}