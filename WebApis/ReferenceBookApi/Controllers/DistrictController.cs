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
public class DistrictController(GenericCrudService<District, DistrictDto, long> crudService) : ApiControllerBase
{
    [HttpGet]
    [ApiGroup("Admin", "Client")]
    //[PermissionAuthorize(UserPermissions.ViewAllDistrict)]
    public Task<ResponseModel<List<DistrictDto>>> GetAll([FromQuery] MetaQueryModel metaQueryModel)
        => crudService.GetAllAsync(metaQueryModel);
    
    [HttpGet("{id:long}")]
    [ApiGroup("Admin", "Client")]
    //PermissionAuthorize(UserPermissions.ViewByIdDistrict)]
    public Task<ResponseModel<DistrictDto>> GetById([FromRoute]long id)
        => crudService.GetByIdAsync(id);

    [HttpPut("{id:long}")]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.UpdateDistrict)]
    public Task<ResponseModel<DistrictDto>> Update([FromBody] DistrictDto dto, [FromRoute] long id)
        => dto.Id != id ? throw new ValidationException("Not valid id") : crudService.OnSaveAsync(dto);
    
    [HttpPost]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.AddDistrict)]
    public Task<ResponseModel<DistrictDto>> Create([FromBody] DistrictDto dto)
        => crudService.OnSaveAsync(dto);
    
    [HttpDelete("{id:long}")]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.RemoveDistrict)]
    public Task<ResponseModel<DistrictDto>> Delete([FromRoute] long id)
    => crudService.DeleteByIdAsync(id);
    
}