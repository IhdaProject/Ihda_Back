using Entity.DataTransferObjects.Role;
using Entity.Exeptions;
using Entity.Models;
using System.ComponentModel.DataAnnotations;
using DatabaseBroker.Repositories.Auth;
using Microsoft.EntityFrameworkCore;

namespace RoleService.Service;

public class RoleService(
    IStructureRepository structureRepository,
    IPermissionRepository permissionsRepository,
    IStructurePermissionRepository structurePermissionsRepository)
    : IRoleService
{
    public async Task<StructureDTO> CreateStructureAsync(
        StructureForCreationDTO structureForCreationDTO)
    {
        ValidationForCreation(structureForCreationDTO);

        var newStructure = new Structure()
        {
            Name = structureForCreationDTO.name,
        };

        var structureModel = await structureRepository.AddAsync(newStructure);

        return new StructureDTO(
            structureModel.Id,
            structureModel.Name);
    }

    public async Task<StructureDTO> ModifyStructureAsync(
        StructureDTO structure)
    {
        Validate(structure);

        var newStructure = await structureRepository.GetByIdAsync(structure.id);

        newStructure.Name = structure.name ?? newStructure.Name;

        var ChangedStructure = await structureRepository.UpdateAsync(newStructure);

        return new StructureDTO(
            ChangedStructure.Id,
            ChangedStructure.Name);
    }

    public async Task<Structure> RemoveStructureAsync(long structureId)
    {
        var newStructure = await structureRepository.GetByIdAsync(structureId);

        Validate(newStructure);

        var Deletedstructure = await structureRepository.RemoveAsync(newStructure);

        return Deletedstructure;
    }

    public async Task<IEnumerable<StructureDTO>> RetrieveStructureAsync()
    {
        var newStructureList = structureRepository.OrderBy(x => x.Id);

        return await newStructureList.Select(structure =>
            new StructureDTO
            (structure.Id,
                structure.Name, structure.IsDefault)).ToListAsync();
    }

    public IQueryable<StructureDTO> RetrieveStructureByName(string Name)
    {
        var newStructure = structureRepository.GetAllAsQueryable()
            .Where(s => s.Name.uz.Contains(Name) ||
                        s.Name.ru.Contains(Name));

        return newStructure.Select(structure =>
            new StructureDTO
            (structure.Id,
                structure.Name, structure.IsDefault));
    }

    private void Validate(Structure structure)
    {
        if (structure is null)
        {
            throw new NotFoundException("Structure not found");
        }
    }

    private void Validate(StructureDTO structure)
    {
        if (structure is null)
        {
            throw new NotFoundException("StructureDTO not found");
        }
    }

    private void ValidationForCreation(StructureForCreationDTO structure)
    {
        if (structure is null)
        {
            throw new ValidationException("StructureForCreationDTO can not be null");
        }
    }

    public async Task<Permission> ModifyPermission(Permission permission)
    {
        var newPermission = await permissionsRepository.GetByIdAsync(permission.Id);

        newPermission.Name = permission.Name ?? newPermission.Name;

        var ChengedPermission = await permissionsRepository.UpdateAsync(newPermission);

        return ChengedPermission;
    }

    public IQueryable<Permission> RetrievePermissionAsync()
    {
        return permissionsRepository.GetAllAsQueryable();
    }

    public IQueryable<Permission> RetrievePermissionByNameAsync(string Name)
    {
        return permissionsRepository.GetAllAsQueryable()
            .Where(permission =>
                permission.Name.uz.Contains(Name) ||
                permission.Name.ru.Contains(Name));
    }
    
    public async Task<StructurePermissionDTO> CreateStructurePermission(
        StructurePermissionForCreationDTO structurePermissionDTO)
    {
        var StructurePermission = new StructurePermission()
        {
            PermissionId = structurePermissionDTO.permissionId,
            StructureId = structurePermissionDTO.structureId,
            GrantedById = 12
        };

        var StructurePermissionValidation = await structurePermissionsRepository.GetAllAsQueryable()
            .Where(sp => sp.StructureId == structurePermissionDTO.structureId &&
                         sp.PermissionId == structurePermissionDTO.permissionId).FirstOrDefaultAsync();


        var newStructurePermission = StructurePermissionValidation ??
                                     await structurePermissionsRepository.AddAsync(StructurePermission);

        return new StructurePermissionDTO(
            newStructurePermission.Id,
            newStructurePermission.StructureId,
            newStructurePermission.PermissionId);
    }

    public async Task<StructurePermissionDTO> RemoveStructurePermissionAsync(
        StructurePermissionForCreationDTO structurePermissionForCreationDTO)
    {
        var structurePermission = await structurePermissionsRepository.GetAllAsQueryable()
            .Where(sp =>
                (sp.StructureId == structurePermissionForCreationDTO.structureId &&
                 sp.PermissionId == structurePermissionForCreationDTO.permissionId)).FirstOrDefaultAsync();

        var newStructurePermission = await structurePermissionsRepository.RemoveAsync(structurePermission);

        return new StructurePermissionDTO(
            newStructurePermission.Id,
            newStructurePermission.StructureId,
            newStructurePermission.PermissionId);
    }

    public IQueryable<StructurePermissionDTO> RetriveStructurePermission()
    {
        var structureList = structurePermissionsRepository.GetAllAsQueryable();

        return structureList.Select(sp =>
            new StructurePermissionDTO(
                sp.Id,
                sp.StructureId,
                sp.PermissionId));
    }

    public IQueryable<StructurePermissionDTO> RetriveStructurePermissionByStructureId(long structureId)
    {
        var structureList =
            structurePermissionsRepository.GetAllAsQueryable()
                .Where(sp => sp.StructureId == structureId);

        return structureList.Select(sp =>
            new StructurePermissionDTO(
                sp.Id,
                sp.StructureId,
                sp.PermissionId));
    }

    public async Task AssignPermissionsToStructureById(long structureId, long assignerId,
        params long[] permissionsIds)
    {
        var structure = await structureRepository.FirstOrDefaultAsync(x => x.Id == structureId);

        NotFoundException.ThrowIfNull(structure);

        await structurePermissionsRepository.RemoveRangeAsync(structure.StructurePermissions.ToArray());

        await structurePermissionsRepository
            .AddRangeAsync(permissionsIds.Select(x => new StructurePermission()
        {
            PermissionId = x,
            GrantedById = assignerId,
            StructureId = structure.Id,
        }).ToArray());
    }
}