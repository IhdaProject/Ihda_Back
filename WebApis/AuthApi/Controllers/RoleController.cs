using Entity.DataTransferObjects.Role;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using RoleService.Service;
using WebCore.Controllers;
using WebCore.Models;

namespace AuthApi.Controllers;
[ApiExplorerSettings(GroupName = "Client")]
public class RoleController(IRoleService structureService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ResponseModel<List<StructureDto>>> GetAllStructures([FromQuery]MetaQueryModel metaQueryModel)
        => ResponseModel<List<StructureDto>>.ResultFromContent(await structureService.RetrieveStructureAsync(metaQueryModel));
    [HttpGet]
    public async Task<ResponseModel<List<StructurePermissionDto>>> GetStructureById([FromQuery]MetaQueryModel metaQueryModel)
        => ResponseModel<List<StructurePermissionDto>>.ResultFromContent(await structureService.RetrieveStructurePermissionByStructureIdAsync(metaQueryModel));
    [HttpPost]
    public async Task<ResponseModel<StructureDto>> CreateStructure(StructureForCreationDto structureDto)
        => ResponseModel<StructureDto>.ResultFromContent(await structureService.CreateStructureAsync(structureDto));
    [HttpPut]
    public async Task<ResponseModel<StructureDto>> EditStructure(StructureDto structure)
        => ResponseModel<StructureDto>.ResultFromContent(await structureService.ModifyStructureAsync(structure));
    [HttpDelete]
    public async Task<ResponseModel<StructurePermissionDto>> DeleteStructurePermission(StructurePermissionForCreationDto structurePermission)
        => ResponseModel<StructurePermissionDto>.ResultFromContent(await structureService.RemoveStructurePermissionAsync(structurePermission));
    [HttpDelete]
    public async Task<ResponseModel<bool>> DeleteStructure(long structureId)
        => ResponseModel<bool>.ResultFromContent(await structureService.RemoveStructureAsync(structureId));
    [HttpGet]
    public async Task<ResponseModel<List<PermissionDto>>> GetPermissions([FromQuery]MetaQueryModel metaQueryModel)
        => ResponseModel<List<PermissionDto>>.ResultFromContent(await structureService.RetrievePermissionAsync(metaQueryModel));
    [HttpPut]
    public async Task<ResponseModel<PermissionDto>> UpdatePermissionName(PermissionDto permissionDto)
        => ResponseModel<PermissionDto>.ResultFromContent(await structureService.ModifyPermissionAsync(permissionDto));
}