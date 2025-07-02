using AutoMapper;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;

namespace Entity.MappingProfile;

public class TrainingCenterMappingProfile : Profile
{
    public TrainingCenterMappingProfile()
    {
        CreateMap<Region, RegionDto>();
        CreateMap<RegionDto, Region>();
    }
}