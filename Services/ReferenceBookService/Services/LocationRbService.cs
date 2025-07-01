using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ApiModels;
using WebCore.Models;

namespace ReferenceBookService.Services;

public class LocationRbService : ILocationRbService
{
    public Task<ResponseModel<List<CountryDto>>> GetCountriesAsync(MetaQueryModel metaQuery)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<List<RegionDto>>> GetRegionsAsync(MetaQueryModel metaQuery)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<List<DistrictDto>>> GetDistrictsAsync(MetaQueryModel metaQuery)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<CountryDto>> OnSaveCountryAsync(CountryDto country)
    {
        throw new NotImplementedException();
    }
}