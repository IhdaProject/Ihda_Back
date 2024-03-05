using Entity.DataTransferObjects.Role;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using RoleService.Service;
using Entity.Enum;
using Microsoft.EntityFrameworkCore;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;

namespace AuthApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RoleController : ApiControllerBase
{
    private readonly IRoleService _structureService;

    public RoleController(IRoleService structureService)
    {
        _structureService = structureService;
    }

    [HttpPost, PermissionAuthorize(UserPermissions.StructureCreate)]
    public async Task<ResponseModel> CreateStructure(StructureForCreationDTO structureDto)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.CreateStructureAsync(structureDto));
    }

    [HttpGet("{name}"), PermissionAuthorize(UserPermissions.StructureView)]
    public async ValueTask<ResponseModel> GetStructureByName(string name)
    {
        return ResponseModel
            .ResultFromContent(
                _structureService.RetrieveStructureByName(name));
    }

    [HttpPut, PermissionAuthorize(UserPermissions.StructureEdit)]
    public async ValueTask<ResponseModel> PutStructure(StructureDTO structure)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.ModifyStructureAsync(structure));
    }

    [HttpGet, PermissionAuthorize(UserPermissions.StructureView)]
    public async ValueTask<ResponseModel> GetAllStructures()
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.RetrieveStructureAsync());
    }

    [HttpPut, PermissionAuthorize(UserPermissions.PermissionNameEdit)]
    public async ValueTask<ResponseModel> UpdatePermissionName(Permission permissionName)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.ModifyPermission(permissionName));
    }

    [HttpGet, PermissionAuthorize(UserPermissions.PermissionView)]
    public async ValueTask<ResponseModel> GetPermissions()
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.RetrievePermissionAsync().ToListAsync());
    }


    [HttpGet, PermissionAuthorize(UserPermissions.PermissionView)]
    public IActionResult GetPermissionByName(string permissionName)
    {
        return Ok(_structureService.RetrieveStructureByName(permissionName));
    }


    [HttpPost, PermissionAuthorize(UserPermissions.StructureEdit)]
    public async ValueTask<ResponseModel> CreateStructurePermission(
        StructurePermissionForCreationDTO structurePermission)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.CreateStructurePermission(structurePermission));
    }

    [HttpGet, PermissionAuthorize(UserPermissions.PermissionView)]
    public IActionResult GetAllStructurePermission()
    {
        return Ok(_structureService.RetriveStructurePermission().ToList());
    }

    [HttpGet("/api/structures/{structureId:long}/permissions"), PermissionAuthorize(UserPermissions.PermissionView)]
    public async ValueTask<ResponseModel> GetStructurePermissionByStructureId(long structureId)
    {
        return ResponseModel
            .ResultFromContent(
                await _structureService.RetriveStructurePermissionByStructureId(structureId).ToListAsync());
    }

    [HttpDelete, PermissionAuthorize(UserPermissions.StructureEdit)]
    public async ValueTask<ActionResult<ResponseModel>> DeleteStructurePermission(
        StructurePermissionForCreationDTO structurePermission)
    {
        return ResponseModel.ResultFromContent(
            await _structureService
                .RemoveStructurePermissionAsync(structurePermission));
    }

    [HttpPost, PermissionAuthorize(UserPermissions.StructureEdit)]
    public async ValueTask<ResponseModel> AssignPermissionsToStructureById(
        [FromBody] AssignPermissionToStructureDto dto)
    {
        await _structureService
            .AssignPermissionsToStructureById(dto.StructureId, 
                this.UserId, dto.PermissionIds);
        return ResponseModel.ResultFromContent(true);
    }
}