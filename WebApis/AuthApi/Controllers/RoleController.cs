using Entity.DataTransferObjects.Role;
using Microsoft.AspNetCore.Mvc;
using RoleService.Service;
using Entity.Models.Auth;
using WebCore.Controllers;
using WebCore.Models;

namespace AuthApi.Controllers;
[ApiExplorerSettings(GroupName = "Client")]
public class RoleController(IRoleService structureService) : ApiControllerBase
{
    [HttpGet]
    //[PermissionAuthorize(UserPermissions.StructureView)]
    public async Task<ResponseModel<List<StructureDto>>> GetAllStructures()
        => ResponseModel<List<StructureDto>>.ResultFromContent(await structureService.RetrieveStructureAsync(""));
    
    [HttpGet]
    //[PermissionAuthorize(UserPermissions.StructureView)]
    public async Task<ResponseModel<List<StructurePermissionDto>>> GetStructureById(long structureId)
        => ResponseModel<List<StructurePermissionDto>>.ResultFromContent(await structureService.RetrieveStructurePermissionByStructureIdAsync(structureId));
    
    [HttpPost]
    //[PermissionAuthorize(UserPermissions.StructureCreate)]
    public async Task<ResponseModel<StructureDto>> CreateStructure(StructureForCreationDto structureDto)
        => ResponseModel<StructureDto>.ResultFromContent(await structureService.CreateStructureAsync(structureDto));
    
    [HttpPut]
    //[PermissionAuthorize(UserPermissions.StructureEdit)]
    public async Task<ResponseModel<StructureDto>> EditStructure(StructureDto structure)
        => ResponseModel<StructureDto>.ResultFromContent(await structureService.ModifyStructureAsync(structure));

    [HttpDelete]
    //[PermissionAuthorize(UserPermissions.StructureEdit)]
    public async Task<ResponseModel<StructurePermissionDto>> DeleteStructurePermission(StructurePermissionForCreationDto structurePermission)
        => ResponseModel<StructurePermissionDto>.ResultFromContent(await structureService.RemoveStructurePermissionAsync(structurePermission));

    [HttpDelete]
    //[PermissionAuthorize(UserPermissions.StructureDelete)]
    public async Task<ResponseModel<bool>> DeleteStructure(long structureId)
        => ResponseModel<bool>.ResultFromContent(await structureService.RemoveStructureAsync(structureId));

    [HttpGet]
    //[PermissionAuthorize(UserPermissions.PermissionView)]
    public async Task<ResponseModel<List<Permission>>> GetPermissions()
        => ResponseModel<List<Permission>>.ResultFromContent(await structureService.RetrievePermissionAsync(""));

    [HttpPut]
    //[PermissionAuthorize(UserPermissions.PermissionNameEdit)]
    public async Task<ResponseModel<Permission>> UpdatePermissionName(Permission permissionName)
        => ResponseModel<Permission>.ResultFromContent(await structureService.ModifyPermissionAsync(permissionName));
}