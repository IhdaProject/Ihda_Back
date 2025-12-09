using System.Net;
using AutoMapper;
using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Exceptions;
using Entity.Models.ApiModels;
using Entity.Models.ReferenceBook;
using Microsoft.EntityFrameworkCore;

namespace ReferenceBookService.Services;

public class LocationRbService(
    GenericRepository<Country, long> countryRepository,
    GenericRepository<Region, long> regionRepository,
    GenericRepository<District, long> districtRepository,
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
    public async Task<ResponseModel<List<RegionDto>>> GetRegionsAsync(MetaQueryModel metaQuery)
    {
        var query = regionRepository.GetAllAsQueryable()
            .FilterByExpressions(metaQuery);

        var items = await query
            .Sort(metaQuery)
            .Paging(metaQuery)
            .Select(c => mapper.Map<RegionDto>(c))
            .ToListAsync();

        var totalCount = await query.CountAsync();

        return ResponseModel<List<RegionDto>>.ResultFromContent(
            items,
            total: totalCount);
    }
    public async Task<ResponseModel<List<DistrictDto>>> GetDistrictsAsync(MetaQueryModel metaQuery)
    {
        var query = districtRepository.GetAllAsQueryable()
            .FilterByExpressions(metaQuery);

        var items = await query
            .Sort(metaQuery)
            .Paging(metaQuery)
            .Select(c => mapper.Map<DistrictDto>(c))
            .ToListAsync();

        var totalCount = await query.CountAsync();

        return ResponseModel<List<DistrictDto>>.ResultFromContent(
            items,
            total: totalCount);
    }
    public async Task<ResponseModel<CountryDto>> OnSaveCountryAsync(CountryDto country)
    {
        if (country.Id == 0)
            return ResponseModel<CountryDto>.ResultFromContent(
                mapper.Map<CountryDto>(
                    await countryRepository.AddWithSaveChangesAsync(
                        mapper.Map<Country>(country))));

        var entity = await countryRepository.GetByIdAsync(country.Id) ?? throw new NotFoundException($"Not found {nameof(country)}");
        mapper.Map(country, entity);
        await countryRepository.UpdateWithSaveChangesAsync(entity);

        return ResponseModel<CountryDto>.ResultFromContent(mapper.Map<CountryDto>(entity));
    }

    public async Task<ResponseModel<RegionDto>> OnSaveRegionAsync(RegionDto region)
    {
        if (region.Id == 0)
            return ResponseModel<RegionDto>.ResultFromContent(
                mapper.Map<RegionDto>(
                    await regionRepository.AddWithSaveChangesAsync(
                        mapper.Map<Region>(region))));

        var entity = await regionRepository.GetByIdAsync(region.Id) ?? throw new NotFoundException($"Not found {nameof(region)}");
        mapper.Map(region, entity);
        await regionRepository.UpdateWithSaveChangesAsync(entity);

        return ResponseModel<RegionDto>.ResultFromContent(mapper.Map<RegionDto>(entity));
    }

    public async Task<ResponseModel<DistrictDto>> OnSaveDistrictAsync(DistrictDto district)
    {
        if (district.Id == 0)
            return ResponseModel<DistrictDto>.ResultFromContent(
                mapper.Map<DistrictDto>(
                    await districtRepository.AddWithSaveChangesAsync(
                        mapper.Map<District>(district))),HttpStatusCode.Created);

        var entity = await districtRepository.GetByIdAsync(district.Id) ?? throw new NotFoundException($"Not found {nameof(district)}");
        mapper.Map(district, entity);
        await districtRepository.UpdateWithSaveChangesAsync(entity);

        return ResponseModel<DistrictDto>.ResultFromContent(mapper.Map<DistrictDto>(entity));
    }
}