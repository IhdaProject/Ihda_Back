using System.ComponentModel.DataAnnotations;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Enums;
using Entity.Models.ApiModels;
using Entity.Models.ReferenceBook;
using Microsoft.AspNetCore.Mvc;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.GeneralServices;


namespace ReferenceBookApi.Controllers;
[Route("api/[controller]")]
public class CountryController(GenericCrudService<Country, CountryDto, long> crudService) : ApiControllerBase

{
    [HttpGet]
    [ApiGroup("Admin", "Client")]
    //[PermissionAuthorize(UserPermissions.ViewAllCountries)]
    public Task<ResponseModel<List<CountryDto>>> GetAll([FromQuery] MetaQueryModel metaQueryModel)
        => crudService.GetAllAsync(metaQueryModel);
    
    [HttpGet("{id:long}")]
    [ApiGroup("Admin", "Client")]
    //[PermissionAuthorize(UserPermissions.ViewByIdCountry)]
    public Task<ResponseModel<CountryDto>> GetById([FromRoute]long id)
        => crudService.GetByIdAsync(id);

    [HttpPut("{id:long}")]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.UpdateCountry)]
    public Task<ResponseModel<CountryDto>> Update([FromBody] CountryDto dto, [FromRoute] long id)
        => dto.Id != id ? throw new ValidationException("Not valid id") : crudService.OnSaveAsync(dto);
    
    [HttpPost]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.AddCountry)]
    public Task<ResponseModel<CountryDto>> Create([FromBody] CountryDto dto)
        => crudService.OnSaveAsync(dto);
    
    [HttpDelete("{id:long}")]
    [ApiGroup("Admin")]
    [PermissionAuthorize(UserPermissions.RemoveCountry)]
    public Task<ResponseModel<CountryDto>> Delete([FromRoute] long id)
        => crudService.DeleteByIdAsync(id);
}