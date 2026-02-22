using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;

namespace QuranCourseService.Services;

public interface ITrainingCenterService
{
    Task<ResponseModel<TrainingCenterDto>> OnSaveTrainingCenterAsync(TrainingCenterDto trainingCenter, long userId);
    Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCentersAsync(MetaQueryModel metaQuery);
    Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCentersByManagerAsync(MetaQueryModel metaQuery, long userId);
    Task<ResponseModel<TrainingCenterDto>> GetTrainingCenterByIdAsync(long id);
    Task<ResponseModel<CourseFormDto>> OnSaveCourseFormAsync(CourseFormDto courseForm, long userId);
    Task<ResponseModel<List<CourseFormDto>>> GetCourseFormsAsync(MetaQueryModel metaQuery);
    Task<ResponseModel<List<CourseFormByManagerDto>>> GetCourseFormsByTrainingCenterAsync(MetaQueryModel metaQuery, long trainingCenterId, long userId);
    Task<ResponseModel<CourseFormDto>> GetCourseFormByIdAsync(long id);
}