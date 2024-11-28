using Entity.DataTransferObjects.Role;
using Entity.Models;

namespace RoleService.Service;

public interface IRoleService
{
    Task<StructureDTO> CreateStructureAsync(StructureForCreationDTO structureForCreationDTO);
    Task<IEnumerable<StructureDTO>> RetrieveStructureAsync();
    IQueryable<StructureDTO> RetrieveStructureByName(string Name);
    Task<StructureDTO> ModifyStructureAsync(StructureDTO structure);
    Task<Structure> RemoveStructureAsync(long structureId);


    // Permission Service
    Task<Permission> ModifyPermission(Permission permission);
    IQueryable<Permission> RetrievePermissionAsync();
    IQueryable<Permission> RetrievePermissionByNameAsync(string Name);


    public Task<StructurePermissionDTO> CreateStructurePermission(
        StructurePermissionForCreationDTO structurePermissionDTO);

    public Task<StructurePermissionDTO> RemoveStructurePermissionAsync(
        StructurePermissionForCreationDTO structurePermissionForCreationDTO);

    public IQueryable<StructurePermissionDTO> RetriveStructurePermission();
    public IQueryable<StructurePermissionDTO> RetriveStructurePermissionByStructureId(long structureId);
    /// <summary>
    /// Agar structure da bor permission lar ham berilgan bo'lsa, o'shalar hisobga olinmaydi 
    /// </summary>
    /// <param name="structureId"></param>
    /// <param name="permissionsIds"></param>
    /// <returns></returns>
    public Task AssignPermissionsToStructureById(long structureId, long assignerId, params long[] permissionsIds);
}