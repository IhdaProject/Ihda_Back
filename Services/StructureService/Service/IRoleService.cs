using Entity.DataTransferObjects.Role;
using Entity.Models.ApiModels;
using Entity.Models.Auth;

namespace RoleService.Service;

public interface IRoleService
{
    Task<ResponseModel<StructureDto>> CreateStructureAsync(StructureForCreationDto structureForCreationDto);
    Task<ResponseModel<StructureDto>> ModifyStructureAsync(StructureForModificationDto structure);
    Task<ResponseModel<bool>> RemoveStructureAsync(long structureId);
    Task<ResponseModel<List<StructureDto>>> RetrieveStructureAsync(MetaQueryModel metaQueryModel);
    Task<ResponseModel<PermissionDto>> ModifyPermissionAsync(PermissionDto permissionDto);
    Task<ResponseModel<List<PermissionDto>>> RetrievePermissionAsync(MetaQueryModel metaQueryModel);
    Task<ResponseModel<List<PermissionDto>>> RetrieveStructurePermissionByStructureIdAsync(MetaQueryModel metaQueryModel);
}