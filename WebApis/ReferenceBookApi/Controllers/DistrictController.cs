using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.GeneralServices;
using WebCore.Models;

public class DistrictController(GenericCrudService<District, DistrictDto, long> crudService) : ApiControllerBase
{
    [HttpGet]
    public Task<ResponseModel<List<DistrictDto>>> GetAllDistricts([FromQuery] MetaQueryModel metaQueryModel)
        => crudService.GetAllAsync(metaQueryModel);
    [HttpGet]
    public Task<ResponseModel<DistrictDto>> GetById([FromRoute]long id)
        => crudService.GetByIdAsync(id);
    [HttpPost]
    public Task<ResponseModel<DistrictDto>> OnSave([FromBody] DistrictDto dto)
        => crudService.OnSaveAsync(dto);
    [HttpPost]
    public Task<ResponseModel<DistrictDto>> Delete([FromBody] long id)
    => crudService.DeleteByIdAsync(id);
    
}