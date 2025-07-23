using Entity.DataTransferObjects.ReferenceBook;
using Entity.Enums;
using Entity.Models.ReferenceBook;
using Microsoft.AspNetCore.Mvc;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.GeneralServices;
using WebCore.Models;

namespace ReferenceBookApi.Controllers;

public class RegionController(GenericCrudService<Region, RegionDto, long> crudService) : ApiControllerBase
{
    [HttpGet]
    [ApiGroup("Admin", "Client")]
    [PermissionAuthorize(UserPermissions.ViewAllRegions)]
    public Task<ResponseModel<List<RegionDto>>> GetAll([FromQuery] MetaQueryModel metaQuery)
        => crudService.GetAllAsync(metaQuery);

    [HttpGet]
    [ApiGroup("Admin", "Client")]
    [PermissionAuthorize(UserPermissions.ViewByIdRegion)]
    public Task<ResponseModel<RegionDto>> GetById([FromRoute] long id)
        => crudService.GetByIdAsync(id);
    
    [HttpPost]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.AddRegion)]
    public Task<ResponseModel<RegionDto>> OnSave([FromBody] RegionDto regionDto)
        => crudService.OnSaveAsync(regionDto);

    [HttpDelete]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.RemoveRegion)]
    public Task<ResponseModel<RegionDto>> Delete([FromBody] long id)
        => crudService.DeleteByIdAsync(id);
}