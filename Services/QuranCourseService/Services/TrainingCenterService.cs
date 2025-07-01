using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;
using WebCore.Models;

namespace QuranCourseService.Services;

public class TrainingCenterService : ITrainingCenterService
{
    public Task<ResponseModel<TrainingCenterDto>> OnSaveTrainingCenterAsync(TrainingCenterDto trainingCenter)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCentersAsync(MetaQueryModel metaQuery)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<TrainingCenterDto>> GetTrainingCenterByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<CourseFormDto>> OnSaveCourseFormAsync(CourseFormDto courseForm)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<List<CourseFormDto>>> GetCourseFormsAsync(MetaQueryModel metaQuery)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<CourseFormDto>> GetCourseFormByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}