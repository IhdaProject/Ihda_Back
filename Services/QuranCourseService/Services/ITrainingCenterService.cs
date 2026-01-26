using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;

namespace QuranCourseService.Services;

public interface ITrainingCenterService
{
    Task<ResponseModel<TrainingCenterDto>> OnSaveTrainingCenterAsync(TrainingCenterDto trainingCenter);
    Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCentersAsync(MetaQueryModel metaQuery);
    Task<ResponseModel<TrainingCenterDto>> GetTrainingCenterByIdAsync(long id);
    Task<ResponseModel<CourseFormDto>> OnSaveCourseFormAsync(CourseFormDto courseForm);
    Task<ResponseModel<List<CourseFormDto>>> GetCourseFormsAsync(MetaQueryModel metaQuery);
    Task<ResponseModel<CourseFormDto>> GetCourseFormByIdAsync(long id);
}