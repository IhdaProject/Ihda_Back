using AutoMapper;
using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.QuranCourses;

namespace Entity.MappingProfile.QuranCourse;

public class TrainingCenterMappingProfile : Profile
{
    public TrainingCenterMappingProfile()
    {
        CreateMap<TrainingCenter, TrainingCenterDto>();
        CreateMap<TrainingCenterDto, TrainingCenter>();
    }
}