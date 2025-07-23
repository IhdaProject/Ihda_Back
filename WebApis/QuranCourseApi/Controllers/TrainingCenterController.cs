using Entity.DataTransferObjects.QuranCourses;
using Entity.Enums;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using QuranCourseService.Services;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;

namespace QuranCourseApi.Controllers;

public class TrainingCenterController(ITrainingCenterService trainingCenterService) : ApiControllerBase
{
    [HttpPost, PermissionAuthorize(UserPermissions.OnSaveTrainingCenter)]
    [ApiGroup("Admin")]
    public async Task<ResponseModel<TrainingCenterDto>> OnSaveTrainingCenter([FromBody]TrainingCenterDto trainingCenter)
        => await trainingCenterService.OnSaveTrainingCenterAsync(trainingCenter);
    [HttpGet, PermissionAuthorize(UserPermissions.ViewTrainingCenters)]
    [ApiGroup("Admin", "Client")]
    public async Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCenter([FromQuery]MetaQueryModel metaQuery)
        => await trainingCenterService.GetTrainingCentersAsync(metaQuery);
    [HttpGet, PermissionAuthorize(UserPermissions.ViewTrainingCenter)]
    [ApiGroup("Admin", "Client")]
    public async Task<ResponseModel<TrainingCenterDto>> GetTrainingCenterById([FromRoute]long id)
        => await trainingCenterService.GetTrainingCenterByIdAsync(id);
    [HttpPost,PermissionAuthorize(UserPermissions.OnSaveCourseForm)]
    [ApiGroup("Admin")]
    public async Task<ResponseModel<CourseFormDto>> OnSaveCourseForm([FromBody]CourseFormDto courseForm)
        => await trainingCenterService.OnSaveCourseFormAsync(courseForm);
    [HttpGet, PermissionAuthorize(UserPermissions.ViewCourseForms)]
    [ApiGroup("Admin", "Client")]
    public async Task<ResponseModel<List<CourseFormDto>>> GetCourseForm([FromQuery]MetaQueryModel metaQuery)
        => await trainingCenterService.GetCourseFormsAsync(metaQuery);
    [HttpGet, PermissionAuthorize(UserPermissions.ViewCourseForm)]
    [ApiGroup("Admin", "Client")]
    public async Task<ResponseModel<CourseFormDto>> GetCourseFormById([FromRoute]long id)
        => await trainingCenterService.GetCourseFormByIdAsync(id);
}