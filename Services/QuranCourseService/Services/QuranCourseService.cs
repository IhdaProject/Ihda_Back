using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;
using WebCore.Models;

namespace QuranCourseService.Services;

public class QuranCourseService : IQuranCourseService
{
    public Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCentersAsync(MetaQueryModel queryModel)
    {
        throw new NotImplementedException();
    }
    public Task<ResponseModel<List<CourseFormDto>>> GetCoursesFormAsync(MetaQueryModel queryModel)
    {
        throw new NotImplementedException();
    }
    public Task<ResponseModel<PetitionForQuranCourseDto>> CreatePetitionAsync(PetitionForQuranCourseDto petition)
    {
        throw new NotImplementedException();
    }
}