using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;
using WebCore.Models;

namespace QuranCourseService.Services;

public interface IQuranCourseService
{
    Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCentersAsync(MetaQueryModel queryModel);
}