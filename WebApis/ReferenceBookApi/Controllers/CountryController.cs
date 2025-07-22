using Entity.DataTransferObjects.ReferenceBook;
using Entity.Enums;
using Entity.Models.ReferenceBook;
using Microsoft.AspNetCore.Mvc;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.GeneralServices;
using WebCore.Models;


namespace ReferenceBookApi.Controllers;
public class CountryController(GenericCrudService<Country, CountryDto, long> crudService) : ApiControllerBase

{
    [HttpGet]
    [PermissionAuthorize(UserPermissions.ViewAllCountries)]
    public Task<ResponseModel<List<CountryDto>>> GetAllDistricts([FromQuery] MetaQueryModel metaQueryModel)
        => crudService.GetAllAsync(metaQueryModel);
    
    [HttpGet]
    [PermissionAuthorize(UserPermissions.ViewByIdCountry)]
    public Task<ResponseModel<CountryDto>> GetById([FromRoute]long id)
        => crudService.GetByIdAsync(id);
    
    [HttpPost]
    [PermissionAuthorize(UserPermissions.AddCountry)]
    public Task<ResponseModel<CountryDto>> OnSave([FromBody] CountryDto dto)
        => crudService.OnSaveAsync(dto);
    
    [HttpPost]
    [PermissionAuthorize(UserPermissions.RemoveCountry)]
    public Task<ResponseModel<CountryDto>> Delete([FromBody] long id)
        => crudService.DeleteByIdAsync(id);
}