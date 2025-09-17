using AutoMapper;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;

namespace Entity.MappingProfile.Location;

public class DistrictMappingProfile : Profile
{
    public DistrictMappingProfile()
    {
        CreateMap<District, DistrictDto>().ForMember(x => x.RegionName, y => y.MapFrom(x => x.Region.Name));
        CreateMap<DistrictDto, District>();
    }
}