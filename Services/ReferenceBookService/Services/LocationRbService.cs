using AutoMapper;
using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ApiModels;
using Entity.Models.ReferenceBook;
using Microsoft.EntityFrameworkCore;
using WebCore.Models;

namespace ReferenceBookService.Services;

public class LocationRbService(GenericRepository<Country, long> countryRepository,
    IMapper mapper) : ILocationRbService
{
    public async Task<ResponseModel<List<CountryDto>>> GetCountriesAsync(MetaQueryModel metaQuery)
    {
        var query = countryRepository.GetAllAsQueryable()
            .FilterByExpressions(metaQuery);

        var items = await query
            .Sort(metaQuery)
            .Paging(metaQuery)
            .Select(c => mapper.Map<CountryDto>(c))
            .ToListAsync();

        var totalCount = await query.CountAsync();

        return ResponseModel<List<CountryDto>>.ResultFromContent(
            items,
            total: totalCount);
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

    public Task<ResponseModel<RegionDto>> OnSaveRegionAsync(RegionDto region)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<DistrictDto>> OnSaveDistrictAsync(DistrictDto district)
    {
        throw new NotImplementedException();
    }
}