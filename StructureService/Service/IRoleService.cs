using Entity.DataTransferObjects.Role;
using Entity.Models;

namespace RoleService.Service;

public interface IRoleService
{
    ValueTask<StructureDTO> CreateStructureAsync(StructureForCreationDTO structureForCreationDTO);
    Task<IEnumerable<StructureDTO>> RetrieveStructureAsync();
    IQueryable<StructureDTO> RetrieveStructureByName(string Name);
    ValueTask<StructureDTO> ModifyStructureAsync(StructureDTO structure);
    ValueTask<Structure> RemoveStructureAsync(long structureId);


    // Permission Service
    ValueTask<Permission> ModifyPermission(Permission permission);
    IQueryable<Permission> RetrievePermissionAsync();
    IQueryable<Permission> RetrievePermissionByNameAsync(string Name);


    public ValueTask<StructurePermissionDTO> CreateStructurePermission(
        StructurePermissionForCreationDTO structurePermissionDTO);

    public ValueTask<StructurePermissionDTO> RemoveStructurePermissionAsync(
        StructurePermissionForCreationDTO structurePermissionForCreationDTO);

    public IQueryable<StructurePermissionDTO> RetriveStructurePermission();
    public IQueryable<StructurePermissionDTO> RetriveStructurePermissionByStructureId(long structureId);
    /// <summary>
    /// Agar structure da bor permission lar ham berilgan bo'lsa, o'shalar hisobga olinmaydi 
    /// </summary>
    /// <param name="structureId"></param>
    /// <param name="permissionsIds"></param>
    /// <returns></returns>
    public ValueTask AssignPermissionsToStructureById(long structureId, long assignerId, params long[] permissionsIds);
}