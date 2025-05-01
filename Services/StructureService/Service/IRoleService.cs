using Entity.DataTransferObjects.Role;
using Entity.Models.ApiModels;
using Entity.Models.Auth;

namespace RoleService.Service;

public interface IRoleService
{
    Task<StructureDto> CreateStructureAsync(StructureForCreationDto structureForCreationDto);
    Task<StructureDto> ModifyStructureAsync(StructureDto structure);
    Task<bool> RemoveStructureAsync(long structureId);
    Task<List<StructureDto>> RetrieveStructureAsync(MetaQueryModel metaQueryModel);
    Task<PermissionDto> ModifyPermissionAsync(PermissionDto permissionDto);
    Task<List<PermissionDto>> RetrievePermissionAsync(MetaQueryModel metaQueryModel);
    Task<StructurePermissionDto> RemoveStructurePermissionAsync(StructurePermissionForCreationDto structurePermissionForCreationDto);
    Task<List<StructurePermissionDto>> RetrieveStructurePermissionByStructureIdAsync(MetaQueryModel metaQueryModel);
}