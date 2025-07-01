using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ApiModels;
using WebCore.Models;

namespace ReferenceBookService.Services;

public interface ILocationRbService
{
    Task<ResponseModel<List<CountryDto>>> GetCountriesAsync(MetaQueryModel metaQuery);
    Task<ResponseModel<List<RegionDto>>> GetRegionsAsync(MetaQueryModel metaQuery);
    Task<ResponseModel<List<DistrictDto>>> GetDistrictsAsync(MetaQueryModel metaQuery);
    Task<ResponseModel<CountryDto>> OnSaveCountryAsync(CountryDto country);
    Task<ResponseModel<RegionDto>> OnSaveRegionAsync(RegionDto region);
    Task<ResponseModel<DistrictDto>> OnSaveDistrictAsync(DistrictDto district);
}