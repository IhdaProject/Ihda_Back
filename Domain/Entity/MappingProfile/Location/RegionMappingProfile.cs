using AutoMapper;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;

namespace Entity.MappingProfile.Location;

public class RegionMappingProfile : Profile
{
    public RegionMappingProfile()
    {
        CreateMap<Region, RegionDto>()
            .ForCtorParam("CountryName", opt => opt.MapFrom(src => src.Country.Name));
        CreateMap<RegionDto, Region>();
    }
}