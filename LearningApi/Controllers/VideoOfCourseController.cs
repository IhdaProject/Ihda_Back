using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace LearningService.Service;

[Route("api/[controller]/[action]")]
[ApiController]
public class VideoOfCourseController :ApiControllerBase
{
    private readonly IVideoOfCourseService _videoOfCourseService;

    public VideoOfCourseController(IVideoOfCourseService videoOfCourseService)
    {
        _videoOfCourseService = videoOfCourseService;
    }

    [HttpPost]
    public async ValueTask<ResponseModel> CreateVideoOfCourse(VideosOfCourseDto Course)
    {
        return ResponseModel
            .ResultFromContent(await _videoOfCourseService.CreateVideoOfCourseAsync(Course));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetVideoOfCourseById(int id)
    {
        return ResponseModel
            .ResultFromContent(await _videoOfCourseService.GetVideoOfCourseByIdAsync(id));
    }
    
    [HttpGet]
    public async ValueTask<ResponseModel> GetVideoOfCourseByCourseId([FromQuery] int courseId,[FromQuery]MetaQueryModel metaQuery)
    {
        if (Request.Query.Count == 1)
            metaQuery.Take = 1000;

        return ResponseModel
            .ResultFromContent(await _videoOfCourseService.GetVideoOfCourseByCourseIdAsync(metaQuery,courseId));
    }
    
    [HttpGet]
    public async ValueTask<ResponseModel> GetAllVideoOfCourse([FromQuery]  MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _videoOfCourseService.GetAllVideoOfCourseAsync(metaQuery));
    }

    [HttpPut]
    public async ValueTask<ResponseModel> UpdateVideoOfCourse(VideoOfCourse Course)
    {
        return ResponseModel
            .ResultFromContent(await _videoOfCourseService.UpdateVideoOfCourseAsync(Course));
    }

    [HttpDelete]
    public async ValueTask<ResponseModel> DeleteVideoOfCourse(int id)
    {
        return ResponseModel
            .ResultFromContent(await _videoOfCourseService.DeleteVideoOfCourseAsync(id));
    }
}
