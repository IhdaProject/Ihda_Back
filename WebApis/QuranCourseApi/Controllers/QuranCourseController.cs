using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using QuranCourseService.Services;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;

namespace QuranCourseApi.Controllers;

[ApiGroup("Client")]
public class QuranCourseController(IQuranCourseService quranCourseService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCenter([FromQuery]MetaQueryModel metaQuery)
        => await quranCourseService.GetTrainingCentersAsync(metaQuery);
    
    [HttpGet]
    public async Task<ResponseModel<List<CourseFormDto>>> GetCoursesForm([FromQuery]MetaQueryModel metaQuery)
        => await quranCourseService.GetCoursesFormAsync(metaQuery);
    
    [HttpPost]
    public async Task<ResponseModel<PetitionForQuranCourseDto>> CreatePetition([FromBody]PetitionForQuranCourseDto petition)
        => await quranCourseService.CreatePetitionAsync(petition);
}