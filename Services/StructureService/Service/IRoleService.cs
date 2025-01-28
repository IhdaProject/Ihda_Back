using Entity.DataTransferObjects.Role;
using Entity.Models.Auth;

namespace RoleService.Service;

public interface IRoleService
{
    Task<StructureDto> CreateStructureAsync(StructureForCreationDto structureForCreationDto);
    Task<StructureDto> ModifyStructureAsync(StructureDto structure);
    Task<bool> RemoveStructureAsync(long structureId);
    Task<List<StructureDto>> RetrieveStructureAsync(string name);
    Task<Permission> ModifyPermissionAsync(Permission permission);
    Task<List<Permission>> RetrievePermissionAsync(string name);
    Task<StructurePermissionDto> RemoveStructurePermissionAsync(StructurePermissionForCreationDto structurePermissionForCreationDto);
    Task<List<StructurePermissionDto>> RetrieveStructurePermissionByStructureIdAsync(long structureId);
}