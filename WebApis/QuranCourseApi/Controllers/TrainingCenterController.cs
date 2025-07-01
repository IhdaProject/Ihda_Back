using Entity.DataTransferObjects.QuranCourses;
using Entity.Enums;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using QuranCourseService.Services;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;


namespace QuranCourseApi.Controllers;

[ApiGroup("Admin")]
public class TrainingCenterController(ITrainingCenterService trainingCenterService) : ApiControllerBase
{
    [HttpPost, PermissionAuthorize(UserPermissions.OnSaveTrainingCenter)]
    public async Task<ResponseModel<TrainingCenterDto>> OnSaveTrainingCenter([FromBody]TrainingCenterDto trainingCenter)
        => await trainingCenterService.OnSaveTrainingCenterAsync(trainingCenter);
    [HttpGet]
    public async Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCenter([FromQuery]MetaQueryModel metaQuery)
        => await trainingCenterService.GetTrainingCentersAsync(metaQuery);
    [HttpGet]
    public async Task<ResponseModel<TrainingCenterDto>> GetTrainingCenterById([FromRoute]long id)
        => await trainingCenterService.GetTrainingCenterByIdAsync(id);
    [HttpPost]
    public async Task<ResponseModel<CourseFormDto>> OnSaveCourseForm([FromBody]CourseFormDto courseForm)
        => await trainingCenterService.OnSaveCourseFormAsync(courseForm);
    [HttpGet]
    public async Task<ResponseModel<List<CourseFormDto>>> GetCourseForm([FromQuery]MetaQueryModel metaQuery)
        => await trainingCenterService.GetCourseFormsAsync(metaQuery);
    [HttpGet]
    public async Task<ResponseModel<CourseFormDto>> GetCourseFormById([FromRoute]long id)
        => await trainingCenterService.GetCourseFormByIdAsync(id);
}