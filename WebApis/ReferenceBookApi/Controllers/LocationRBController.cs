using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using ReferenceBookService.Services;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;

namespace ReferenceBookApi.Controllers;

public class LocationRbController(ILocationRbService locationRbService) : ApiControllerBase
{
    [HttpGet]
    [ApiGroup("Client", "Admin")]
    public Task<ResponseModel<List<CountryDto>>> GetCountries([FromQuery]MetaQueryModel metaQuery)
        => locationRbService.GetCountriesAsync(metaQuery);
    [HttpPost]
    [ApiGroup("Admin")]
    public Task<ResponseModel<CountryDto>> OnSaveCountry([FromBody]CountryDto country)
        => locationRbService.OnSaveCountryAsync(country);
    [HttpGet]
    [ApiGroup("Client", "Admin")]
    public Task<ResponseModel<List<RegionDto>>> GetRegions([FromQuery]MetaQueryModel metaQuery)
        => locationRbService.GetRegionsAsync(metaQuery);
    [HttpPost]
    [ApiGroup("Admin")]
    public Task<ResponseModel<RegionDto>> OnSaveRegion([FromBody]RegionDto region)
        => locationRbService.OnSaveRegionAsync(region);
    [HttpGet]
    [ApiGroup("Client", "Admin")]
    public Task<ResponseModel<List<DistrictDto>>> GetDistricts([FromQuery]MetaQueryModel metaQuery)
        => locationRbService.GetDistrictsAsync(metaQuery);
    [HttpPost]
    [ApiGroup("Admin")]
    public Task<ResponseModel<DistrictDto>> OnSaveDistrict([FromBody]DistrictDto district)
        => locationRbService.OnSaveDistrictAsync(district);
}