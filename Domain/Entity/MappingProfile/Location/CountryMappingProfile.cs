using AutoMapper;
using Entity.DataTransferObjects.QuranCourses;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.QuranCourses;
using Entity.Models.ReferenceBook;

namespace Entity.MappingProfile.Location;

public class TrainingCenterMappingProfile : Profile
{
    public TrainingCenterMappingProfile()
    {
        CreateMap<TrainingCenter, TrainingCenterDto>();
        CreateMap<TrainingCenterDto, TrainingCenter>();
    }
}