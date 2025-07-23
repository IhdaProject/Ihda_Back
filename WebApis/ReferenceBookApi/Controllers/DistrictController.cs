using Entity.DataTransferObjects.ReferenceBook;
using Entity.Enums;
using Entity.Models.ReferenceBook;
using Microsoft.AspNetCore.Mvc;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.GeneralServices;
using WebCore.Models;

namespace ReferenceBookApi.Controllers;

public class DistrictController(GenericCrudService<District, DistrictDto, long> crudService) : ApiControllerBase
{
    [HttpGet]
    [ApiGroup("Admin", "Client")]
    [PermissionAuthorize(UserPermissions.ViewAllDistrict)]
    public Task<ResponseModel<List<DistrictDto>>> GetAll([FromQuery] MetaQueryModel metaQueryModel)
        => crudService.GetAllAsync(metaQueryModel);
    
    [HttpGet]
    [ApiGroup("Admin", "Client")]
    [PermissionAuthorize(UserPermissions.ViewByIdDistrict)]
    public Task<ResponseModel<DistrictDto>> GetById([FromRoute]long id)
        => crudService.GetByIdAsync(id);
    
    [HttpPost]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.AddDistrict)]
    public Task<ResponseModel<DistrictDto>> OnSave([FromBody] DistrictDto dto)
        => crudService.OnSaveAsync(dto);
    
    [HttpPost]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.RemoveDistrict)]
    public Task<ResponseModel<DistrictDto>> Delete([FromBody] long id)
    => crudService.DeleteByIdAsync(id);
    
}