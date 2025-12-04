using Entity.DataTransferObjects.Role;
using Entity.Enums;
using Entity.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RoleService.Service;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;

namespace AuthApi.Controllers;
[ApiGroup("Admin")]
public class RoleController(IRoleService structureService) : ApiControllerBase
{
    [HttpGet]
    [PermissionAuthorize(UserPermissions.ViewStructures)]
    public async Task<ResponseModel<List<StructureDto>>> GetAllStructures([FromQuery]MetaQueryModel metaQueryModel)
        => await structureService.RetrieveStructureAsync(metaQueryModel);
    [HttpGet]
    [PermissionAuthorize(UserPermissions.ViewStructure)]
    public async Task<ResponseModel<List<PermissionDto>>> GetStructureById([FromQuery]MetaQueryModel metaQueryModel)
        => await structureService.RetrieveStructurePermissionByStructureIdAsync(metaQueryModel);
    [HttpPost]
    [PermissionAuthorize(UserPermissions.CreateStructure)]
    public async Task<ResponseModel<StructureDto>> CreateStructure(StructureForCreationDto structureDto)
        => await structureService.CreateStructureAsync(structureDto);
    [HttpPut("{id:long}")]
    [PermissionAuthorize(UserPermissions.UpdateStructure)]
    public async Task<ResponseModel<StructureDto>> EditStructure(StructureForModificationDto structure, long id)
        => id != structure.Id ? throw new ValidationException("Not Valid id") : await structureService.ModifyStructureAsync(structure);
    [HttpDelete("{structureId:long}")]
    [PermissionAuthorize(UserPermissions.RemoveStructure)]
    public async Task<ResponseModel<bool>> DeleteStructure(long structureId)
        => await structureService.RemoveStructureAsync(structureId);
    [HttpGet]
    [PermissionAuthorize(UserPermissions.ViewPermissions)]
    public async Task<ResponseModel<List<PermissionDto>>> GetPermissions([FromQuery]MetaQueryModel metaQueryModel)
        => await structureService.RetrievePermissionAsync(metaQueryModel);
    [HttpPut("{id:long}")]
    [PermissionAuthorize(UserPermissions.UpdatePermission)]
    public async Task<ResponseModel<PermissionDto>> UpdatePermissionName(PermissionDto permissionDto, [FromRoute] long id)
        => permissionDto.Id != id ? throw new ValidationException("Not valid id"): await structureService.ModifyPermissionAsync(permissionDto);
}