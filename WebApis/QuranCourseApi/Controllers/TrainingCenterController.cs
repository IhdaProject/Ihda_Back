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
    [ApiGroup("Admin")]
    [HttpPost, PermissionAuthorize(UserPermissions.OnSaveTrainingCenter)]
    public async Task<ResponseModel<TrainingCenterDto>> OnSaveTrainingCenter([FromBody]TrainingCenterDto trainingCenter)
        => await trainingCenterService.OnSaveTrainingCenterAsync(trainingCenter);
    [ApiGroup("Admin", "Client")]
    //[HttpGet, PermissionAuthorize(UserPermissions.ViewTrainingCenters)]
    public async Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCenter([FromQuery]MetaQueryModel metaQuery)
        => await trainingCenterService.GetTrainingCentersAsync(metaQuery);
    [ApiGroup("Admin", "Client")]
    //[HttpGet, PermissionAuthorize(UserPermissions.ViewTrainingCenter)]
    public async Task<ResponseModel<TrainingCenterDto>> GetTrainingCenterById([FromRoute]long id)
        => await trainingCenterService.GetTrainingCenterByIdAsync(id);
    [HttpPost,PermissionAuthorize(UserPermissions.OnSaveCourseForm)]
    [ApiGroup("Admin")]
    public async Task<ResponseModel<CourseFormDto>> OnSaveCourseForm([FromBody]CourseFormDto courseForm)
        => await trainingCenterService.OnSaveCourseFormAsync(courseForm);
    [ApiGroup("Admin", "Client")]
    //[HttpGet, PermissionAuthorize(UserPermissions.ViewCourseForms)]
    public async Task<ResponseModel<List<CourseFormDto>>> GetCourseForm([FromQuery]MetaQueryModel metaQuery)
        => await trainingCenterService.GetCourseFormsAsync(metaQuery);
    [ApiGroup("Admin", "Client")]
    //[HttpGet, PermissionAuthorize(UserPermissions.ViewCourseForm)]
    public async Task<ResponseModel<CourseFormDto>> GetCourseFormById([FromRoute]long id)
        => await trainingCenterService.GetCourseFormByIdAsync(id);
}