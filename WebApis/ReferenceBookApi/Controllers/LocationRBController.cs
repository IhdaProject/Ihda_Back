using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ApiModels;
using Entity.Models.ReferenceBook;
using Microsoft.AspNetCore.Mvc;
using ReferenceBookService.Services;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.GeneralServices;
using WebCore.Models;

namespace ReferenceBookApi.Controllers;

public class LocationRbController(ILocationRbService locationRbService,
    ICountryService countryService,
    GenericCrudService<Region, RegionDto, long> regionService) : ApiControllerBase
{
    [HttpGet]
    [ApiGroup("Client", "Admin")]
    public Task<ResponseModel<List<CountryDto>>> GetCountries([FromQuery]MetaQueryModel metaQuery)
        => countryService.GetAllAsync(metaQuery);
    [HttpPost]
    [ApiGroup("Admin")]
    public Task<ResponseModel<CountryDto>> OnSaveCountry([FromBody]CountryDto country)
        => countryService.OnSaveAsync(country);
    [HttpGet]
    [ApiGroup("Client", "Admin")]
    public Task<ResponseModel<List<RegionDto>>> GetRegions([FromQuery]MetaQueryModel metaQuery)
        => regionService.GetAllAsync(metaQuery);
    [HttpPost]
    [ApiGroup("Admin")]
    public Task<ResponseModel<RegionDto>> OnSaveRegion([FromBody]RegionDto region)
        => regionService.OnSaveAsync(region);
    [HttpGet]
    [ApiGroup("Client", "Admin")]
    public Task<ResponseModel<List<DistrictDto>>> GetDistricts([FromQuery]MetaQueryModel metaQuery)
        => locationRbService.GetDistrictsAsync(metaQuery);
    [HttpPost]
    [ApiGroup("Admin")]
    public Task<ResponseModel<DistrictDto>> OnSaveDistrict([FromBody]DistrictDto district)
        => locationRbService.OnSaveDistrictAsync(district);
}