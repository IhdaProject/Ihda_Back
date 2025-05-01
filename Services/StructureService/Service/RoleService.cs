using DatabaseBroker.Extensions;
using Entity.DataTransferObjects.Role;
using Entity.Exeptions;
using Entity.Models;
using DatabaseBroker.Repositories.Auth;
using Entity.Models.ApiModels;
using Entity.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace RoleService.Service;

public class RoleService(
    IStructureRepository structureRepository,
    IPermissionRepository permissionsRepository,
    IStructurePermissionRepository structurePermissionsRepository)
    : IRoleService
{
    public async Task<StructureDto> CreateStructureAsync(StructureForCreationDto structureForCreationDto)
    {
        var newStructure = new Structure
        {
            Name = structureForCreationDto.Name,
            StructurePermissions = structureForCreationDto.PermissionIds.Select(p => new StructurePermission()
            {
                PermissionId = p
            }).ToList()
        };

        var structureModel = await structureRepository.AddAsync(newStructure);

        return new StructureDto(
            structureModel.Id,
            structureModel.Name,
            structureModel.StructurePermissions
                .Select(p =>
                    new PermissionDto(
                        p.PermissionId,
                        p.Permission.Code,
                        p.Permission.Name))
                .ToList());
    }
    public async Task<StructureDto> ModifyStructureAsync(StructureDto structure)
    {
        var newStructure = await structureRepository.GetByIdAsync(structure.Id)
            ?? throw new NotFoundException("Not found structure");

        newStructure.Name = structure.Name;

        var changedStructure = await structureRepository.UpdateAsync(newStructure);

        return new StructureDto(
            changedStructure.Id,
            changedStructure.Name,
            changedStructure.StructurePermissions
                .Select(p =>
                    new PermissionDto(
                        p.PermissionId,
                        p.Permission.Code,
                        p.Permission.Name))
                .ToList());
    }
    public async Task<bool> RemoveStructureAsync(long structureId)
    {
        var deletedStructure = await structureRepository.RemoveAsync(new Structure()
                               {
                                   Id = structureId
                               }) ?? throw new NotFoundException("Not found structure");;

        return true;
    }
    public async Task<List<StructureDto>> RetrieveStructureAsync(MetaQueryModel metaQueryModel)
    {
        var query = structureRepository
            .GetAllAsQueryable()
            .FilterByExpressions(metaQueryModel);

        return await query.Select(s =>
            new StructureDto(
                s.Id,
                s.Name,
                s.StructurePermissions
                    .Select(p =>
                        new PermissionDto(
                            p.PermissionId,
                            p.Permission.Code,
                            p.Permission.Name))
                    .ToList()))
            .ToListAsync();
    }
    public async Task<PermissionDto> ModifyPermissionAsync(PermissionDto permissionDto)
    {
        var newPermission = await permissionsRepository.GetByIdAsync(permissionDto.Id)
            ?? throw new NotFoundException("Not found permission");

        newPermission.Name = permissionDto.Name;

        var changedPermission = await permissionsRepository.UpdateAsync(newPermission);

        return permissionDto;
    }

    public Task<List<PermissionDto>> RetrievePermissionAsync(MetaQueryModel metaQueryModel)
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
    public Task<List<StructurePermissionDto>> RetrieveStructurePermissionByStructureIdAsync(MetaQueryModel metaQueryModel)
    {
        throw new NotImplementedException();
    }
}