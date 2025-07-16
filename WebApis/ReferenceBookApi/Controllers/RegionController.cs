using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.GeneralServices;
using WebCore.Models;

public class RegionController(GenericCrudService<Region, RegionDto, long> crudService) : ApiControllerBase
{
    [HttpGet]
    public Task<ResponseModel<List<RegionDto>>> GetAllRegions([FromQuery] MetaQueryModel metaQuery)
        => crudService.GetAllAsync(metaQuery);

    [HttpGet]
    public Task<ResponseModel<RegionDto>> GetById([FromRoute] long id)
        => crudService.GetByIdAsync(id);
    
    [HttpPost]
    public Task<ResponseModel<RegionDto>> OnSave([FromBody] RegionDto regionDto)
        => crudService.OnSaveAsync(regionDto);

    [HttpDelete]
    public Task<ResponseModel<RegionDto>> Delete([FromBody] long id)
        => crudService.DeleteByIdAsync(id);
}