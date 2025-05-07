using Entity.DataTransferObjects.QuranCourses;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using QuranCourseService.Services;
using WebCore.Controllers;
using WebCore.Models;

namespace QuranCourseApi.Controllers;

[ApiExplorerSettings(GroupName = "Client")]
public class QuranCourseController(IQuranCourseService quranCourseService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ResponseModel<List<TrainingCenterDto>>> GetTrainingCenter([FromQuery]MetaQueryModel metaQuery)
        => await quranCourseService.GetTrainingCentersAsync(metaQuery);
}