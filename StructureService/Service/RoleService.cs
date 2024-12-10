using Entity.DataTransferObjects.Role;
using Entity.Exeptions;
using Entity.Models;
using DatabaseBroker.Repositories.Auth;
using Entity.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace RoleService.Service;

public class RoleService(
    IStructureRepository structureRepository,
    IPermissionRepository permissionsRepository,
    IStructurePermissionRepository structurePermissionsRepository)
    : IRoleService
{
    public async Task<StructureDto> CreateStructureAsync(
        StructureForCreationDto structureForCreationDto)
    {
        var newStructure = new Structure
        {
            Name = structureForCreationDto.Name,
        };

        var structureModel = await structureRepository.AddAsync(newStructure);

        return new StructureDto(
            structureModel.Id,
            structureModel.Name);
    }

    public async Task<StructureDto> ModifyStructureAsync(
        StructureDto structure)
    {
        var newStructure = await structureRepository.GetByIdAsync(structure.Id)
            ?? throw new NotFoundException("Not found structure");

        newStructure.Name = structure.Name;

        var changedStructure = await structureRepository.UpdateAsync(newStructure);

        return new StructureDto(
            changedStructure.Id,
            changedStructure.Name);
    }

    public async Task<bool> RemoveStructureAsync(long structureId)
    {
        var deletedStructure = await structureRepository.RemoveAsync(new Structure()
                               {
                                   Id = structureId
                               }) ?? throw new NotFoundException("Not found structure");;

        return true;
    }

    public Task<List<StructureDto>> RetrieveStructureAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<StructureDto>> RetrieveStructureAsync()
    {
        var newStructureList = structureRepository
            .OrderBy(x => x.Id);

        return await newStructureList.Select(structure =>
            new StructureDto(
                structure.Id,
                structure.Name))
            .ToListAsync();
    }
    public async Task<Permission> ModifyPermissionAsync(Permission permission)
    {
        var newPermission = await permissionsRepository.GetByIdAsync(permission.Id)
            ?? throw new NotFoundException("Not found permission");

        newPermission.Name = permission.Name;

        var changedPermission = await permissionsRepository.UpdateAsync(newPermission);

        return changedPermission;
    }

    public Task<List<Permission>> RetrievePermissionAsync(string name)
    {
        throw new NotImplementedException();
    }
    public async Task<StructurePermissionDto> RemoveStructurePermissionAsync(
        StructurePermissionForCreationDto structurePermissionForCreationDto)
    {
        var structurePermission = await structurePermissionsRepository.GetAllAsQueryable()
            .Where(sp => sp.StructureId == structurePermissionForCreationDto.StructureId &&
                         sp.PermissionId == structurePermissionForCreationDto.PermissionId)
            .FirstOrDefaultAsync() ?? throw new NotFoundException("Not found permission");

        var newStructurePermission = await structurePermissionsRepository.RemoveAsync(structurePermission);

        return new StructurePermissionDto(
            newStructurePermission.Id,
            newStructurePermission.StructureId,
            newStructurePermission.PermissionId);
    }
    public Task<List<StructurePermissionDto>> RetrieveStructurePermissionByStructureIdAsync(long structureId)
    {
        throw new NotImplementedException();
    }
}