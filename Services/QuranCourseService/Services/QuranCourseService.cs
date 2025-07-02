using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;
using Entity.Models.QuranCourses;
using WebCore.Models;

namespace QuranCourseService.Services;

public class QuranCourseService(GenericRepository<TrainingCenter, long> trainingRepo) : IQuranCourseService
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