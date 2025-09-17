using Entity.DataTransferObjects.QuranCourses;
using Entity.Enums;
using Entity.Exceptions;
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
    [HttpPost, PermissionAuthorize(UserPermissions.CreateTrainingCenter)]
    public Task<ResponseModel<TrainingCenterDto>> CreateTrainingCenter(
        [FromBody] TrainingCenterDto trainingCenter)
        => trainingCenterService.OnSaveTrainingCenterAsync(trainingCenter);
    
    [ApiGroup("Admin")]
    [HttpPut("{id:long}"), PermissionAuthorize(UserPermissions.UpdateTrainingCenter)]
    public Task<ResponseModel<TrainingCenterDto>> UpdateTrainingCenter(
        [FromBody] TrainingCenterDto trainingCenter, [FromRoute] long id)
        => trainingCenter.Id != id ? throw new ValidationException("Not valid id") : trainingCenterService.OnSaveTrainingCenterAsync(trainingCenter);
    
    [ApiGroup("Admin", "Client")]
    [HttpGet] 
    //[PermissionAuthorize(UserPermissions.ViewTrainingCenters)]
    public async Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCenter([FromQuery]MetaQueryModel metaQuery)
        => await trainingCenterService.GetTrainingCentersAsync(metaQuery);
    [ApiGroup("Admin", "Client")]
    [HttpGet("{id:long}")]
    //[PermissionAuthorize(UserPermissions.ViewTrainingCenter)]
    public async Task<ResponseModel<TrainingCenterDto>> GetTrainingCenterById([FromRoute]long id)
        => await trainingCenterService.GetTrainingCenterByIdAsync(id);

    [HttpPost, PermissionAuthorize(UserPermissions.CreateCourseForm)]
    [ApiGroup("Admin")]
    public Task<ResponseModel<CourseFormDto>> CreateCourseForm([FromBody] CourseFormDto courseForm)
        => trainingCenterService.OnSaveCourseFormAsync(courseForm);
    
    [HttpPut("{id:long}"), PermissionAuthorize(UserPermissions.UpdateCourseForm)]
    [ApiGroup("Admin")]
    public Task<ResponseModel<CourseFormDto>> UpdateCourseForm([FromBody] CourseFormDto courseForm,
        [FromRoute] long id)
        => courseForm.Id != id ? throw new ValidationException("Not valid id") : trainingCenterService.OnSaveCourseFormAsync(courseForm);
    
    [ApiGroup("Admin", "Client")]
    [HttpGet]
    //[PermissionAuthorize(UserPermissions.ViewCourseForms)]
    public async Task<ResponseModel<List<CourseFormDto>>> GetCourseForm([FromQuery]MetaQueryModel metaQuery)
        => await trainingCenterService.GetCourseFormsAsync(metaQuery);
    [ApiGroup("Admin", "Client")]
    [HttpGet("{id:long}")]
    //[PermissionAuthorize(UserPermissions.ViewCourseForm)]
    public async Task<ResponseModel<CourseFormDto>> GetCourseFormById([FromRoute]long id)
        => await trainingCenterService.GetCourseFormByIdAsync(id);
}