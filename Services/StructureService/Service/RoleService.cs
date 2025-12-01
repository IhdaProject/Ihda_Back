using DatabaseBroker.Extensions;
using Entity.DataTransferObjects.Role;
using DatabaseBroker.Repositories.Auth;
using Entity.Exceptions;
using Entity.Models.Auth;
using Microsoft.EntityFrameworkCore;
using WebCore.Models;

namespace RoleService.Service;

public class RoleService(
    IStructureRepository structureRepository,
    IPermissionRepository permissionsRepository,
    IStructurePermissionRepository structurePermissionsRepository)
    : IRoleService
{
    public async Task<ResponseModel<StructureDto>> CreateStructureAsync(StructureForCreationDto structureForCreationDto)
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
            structureModel.Name);
    }
    public async Task<ResponseModel<StructureDto>> ModifyStructureAsync(StructureForModificationDto structure)
    {
        var newStructure = await structureRepository.GetAllAsQueryable()
                .Where(s => s.Id == structure.Id)
                .Include(s => 
                    s.StructurePermissions.Where(sp => !sp.Permission.IsDelete))
                .FirstOrDefaultAsync()
            ?? throw new NotFoundException("Not found structure");

        newStructure.Name = structure.Name;

        newStructure.StructurePermissions = [..newStructure.StructurePermissions.Select(sp =>
        {
            sp.IsDelete = !structure.PermissionIds.Contains(sp.PermissionId);
            return sp;
        })];
            
        await structurePermissionsRepository.AddRangeAsync([..structure.PermissionIds.Where(p => newStructure.StructurePermissions.All(sp => sp.PermissionId != p))
            .Select(p => new StructurePermission()
            {
                PermissionId = p,
                StructureId = newStructure.Id
            })]);

        var changedStructure = await structureRepository.UpdateAsync(newStructure);

        return new StructureDto(
            changedStructure.Id,
            changedStructure.Name);
    }
    public async Task<ResponseModel<bool>> RemoveStructureAsync(long structureId)
    {
        var deletedStructure = await structureRepository.RemoveAsync(new Structure()
                               {
                                   Id = structureId
                               }) ?? throw new NotFoundException("Not found structure");;

        return true;
    }
    public async Task<ResponseModel<List<StructureDto>>> RetrieveStructureAsync(MetaQueryModel metaQueryModel)
    {
        var query = structureRepository
            .GetAllAsQueryable()
            .FilterByExpressions(metaQueryModel);

        return ResponseModel<List<StructureDto>>.ResultFromContent(await query
            .Sort(metaQueryModel)
            .Paging(metaQueryModel)
            .Select(s =>
                new StructureDto(
                    s.Id,
                    s.Name))
                .ToListAsync(), total: await query.CountAsync());
    }
    public async Task<ResponseModel<PermissionDto>> ModifyPermissionAsync(PermissionDto permissionDto)
    {
        var newPermission = await permissionsRepository.GetByIdAsync(permissionDto.Id)
            ?? throw new NotFoundException("Not found permission");

        newPermission.Name = permissionDto.Name;

        var changedPermission = await permissionsRepository.UpdateAsync(newPermission);

        return permissionDto;
    }
    public async Task<ResponseModel<List<PermissionDto>>> RetrievePermissionAsync(MetaQueryModel metaQueryModel)
    {
        var query = permissionsRepository.GetAllAsQueryable()
            .FilterByExpressions(metaQueryModel);
        
        return ResponseModel<List<PermissionDto>>.ResultFromContent(await query
            .Select(p => new PermissionDto(p.Id, p.Code, p.Name))
            .Paging(metaQueryModel)
            .ToListAsync(), total: await query.CountAsync());
    }
    public async Task<ResponseModel<List<PermissionDto>>> RetrieveStructurePermissionByStructureIdAsync(MetaQueryModel metaQueryModel)
    {
        var query = structurePermissionsRepository.GetAllAsQueryable()
            .FilterByExpressions(metaQueryModel);

        var items = await query
            .Sort(metaQueryModel)
            .Paging(metaQueryModel)
            .Select(sp => new PermissionDto(sp.PermissionId,
                sp.Permission.Code, sp.Permission.Name))
            .ToListAsync();
        
        var count = await query.CountAsync();
        
        return ResponseModel<List<PermissionDto>>.ResultFromContent(items,total: count);
    }
}