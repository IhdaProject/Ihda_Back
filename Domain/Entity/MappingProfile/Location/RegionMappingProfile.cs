using AutoMapper;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;

namespace Entity.MappingProfile.Location;

public class RegionMappingProfile : Profile
{
    public RegionMappingProfile()
    {
        CreateMap<Region, RegionDto>().ForMember(x => x.CountryName, y => y.MapFrom(x => x.Country.Name));
        CreateMap<RegionDto, Region>();
    }
}