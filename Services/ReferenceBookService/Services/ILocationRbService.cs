using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ApiModels;

namespace ReferenceBookService.Services;

public interface ILocationRbService
{
    Task<ResponseModel<List<RegionDto>>> GetRegionsAsync(MetaQueryModel metaQuery);
    Task<ResponseModel<List<DistrictDto>>> GetDistrictsAsync(MetaQueryModel metaQuery);
    Task<ResponseModel<RegionDto>> OnSaveRegionAsync(RegionDto region);
    Task<ResponseModel<DistrictDto>> OnSaveDistrictAsync(DistrictDto district);
}