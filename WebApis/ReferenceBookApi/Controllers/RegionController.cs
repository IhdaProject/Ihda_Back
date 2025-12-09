using Entity.DataTransferObjects.ReferenceBook;
using Entity.Enums;
using Entity.Exceptions;
using Entity.Models.ApiModels;
using Entity.Models.ReferenceBook;
using Microsoft.AspNetCore.Mvc;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.GeneralServices;

namespace ReferenceBookApi.Controllers;
[Route("api/[controller]")]
public class RegionController(GenericCrudService<Region, RegionDto, long> crudService) : ApiControllerBase
{
    [HttpGet]
    [ApiGroup("Admin", "Client")]
    //[PermissionAuthorize(UserPermissions.ViewAllRegions)]
    public Task<ResponseModel<List<RegionDto>>> GetAll([FromQuery] MetaQueryModel metaQuery)
        => crudService.GetAllAsync(metaQuery);

    [HttpGet("{id:long}")]
    [ApiGroup("Admin", "Client")]
    //[PermissionAuthorize(UserPermissions.ViewByIdRegion)]
    public Task<ResponseModel<RegionDto>> GetById([FromRoute] long id)
        => crudService.GetByIdAsync(id);
    
    [HttpPost]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.AddRegion)]
    public Task<ResponseModel<RegionDto>> Create([FromBody] RegionDto regionDto)
        => crudService.OnSaveAsync(regionDto);
    
    [HttpPut("{id:long}")]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.UpdateRegion)]
    public Task<ResponseModel<RegionDto>> Update([FromBody] RegionDto regionDto, [FromRoute] long id)
        =>regionDto.Id != id ? throw new ValidationException("Not valid id") : crudService.OnSaveAsync(regionDto);

    [HttpDelete("{id:long}")]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.RemoveRegion)]
    public Task<ResponseModel<RegionDto>> Delete([FromRoute] long id)
        => crudService.DeleteByIdAsync(id);
}