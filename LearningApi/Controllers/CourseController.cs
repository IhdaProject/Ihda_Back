using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using LearningService.Service;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace LearningApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CourseController : ApiControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    [HttpPost]
    public async ValueTask<ResponseModel> CreateCourse(CourseDto course)
    {
        return ResponseModel
            .ResultFromContent(await _courseService.CreateCourseAsync(course));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetAllCourse([FromQuery] MetaQueryModel metaQuery)
    {
        if (Request.Query.Count == 0)
            metaQuery.Take = 1000;

        return ResponseModel
            .ResultFromContent(await _courseService.GetAllCourseAsync(metaQuery));
    }
    [HttpGet]
    public async ValueTask<ResponseModel> GetAllCourseByCategoryId([FromQuery] MetaQueryModel metaQuery,[FromQuery]int categoryId)
    {
        if (Request.Query.Count == 0)
            metaQuery.Take = 1000;

        return ResponseModel
            .ResultFromContent(await _courseService.GetAllCourseByCategoryIdAsync(metaQuery,categoryId));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetCourseById(int id)
    {
        return ResponseModel
            .ResultFromContent(await _courseService.GetCourseByIdAsync(id));
    }

    [HttpPut]
    public async ValueTask<ResponseModel> UpdateCourse(Course course)
    {
        return ResponseModel
            .ResultFromContent(await _courseService.UpdateCourseAsync(course));
    }

    [HttpDelete]
    public async ValueTask<ResponseModel> DeleteCourse(int id)
    {
        return ResponseModel
            .ResultFromContent(await _courseService.DeleteCourseAsync(id));
    }
}
