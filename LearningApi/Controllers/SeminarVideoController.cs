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
public class SeminarVideoController : ApiControllerBase
{
    private readonly ISeminarVideoService _seminarVideoService;

    public SeminarVideoController(ISeminarVideoService seminarVideoService)
    {
        _seminarVideoService = seminarVideoService;
    }

    [HttpPost]
    public async ValueTask<ResponseModel> CreateSeminarVideo(SeminarVideoDto Course)
    {
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.CreateSeminarVideoAsync(Course));
    }

    [HttpPost]
    public async ValueTask<ResponseModel> CreateCategory(CategoryDto Course)
    {
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.CreateSeminarVideoCategoryAsync(Course));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetAllCategory([FromQuery] MetaQueryModel metaQuery)
    {
        if (Request.Query.Count == 0)
            metaQuery.Take = 1000;
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.GetAllSeminarVideoCategoryAsync(metaQuery));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetSeminarVideoByCategoryId([FromQuery] MetaQueryModel metaQuery,int categoryId)
    {
        if (Request.Query.Count == 1)
            metaQuery.Take = 1000;
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.GetSeminarVideoByCategoryIdAsync(metaQuery,categoryId));
    }
    
    [HttpGet]
    public async ValueTask<ResponseModel> GetAllSeminarVideo([FromQuery] MetaQueryModel metaQuery)
    {
        if (Request.Query.Count == 0)
            metaQuery.Take = 1000;
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.GetAllSeminarVideoAsync(metaQuery));
    }
    
    [HttpGet]
    public async ValueTask<ResponseModel> GetSeminarVideoWithDetails([FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.GetSeminarVideoWithDetailsAsync(metaQuery));
    }


    [HttpPut]
    public async ValueTask<ResponseModel> UpdateSeminarVideo(SeminarVideo Course)
    {
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.UpdateSeminarVideoAsync(Course));
    }
    
    [HttpPut]
    public async ValueTask<ResponseModel> UpdateCategory(Category Course)
    {
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.UpdateSeminarVideoCategoryAsync(Course));
    }

    [HttpDelete]
    public async ValueTask<ResponseModel> DeleteSeminarVideo(int id)
    {
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.DeleteSeminarVideoAsync(id));
    }
    
    [HttpDelete]
    public async ValueTask<ResponseModel> DeleteCategory(int id)
    {
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.DeleteSeminarVideoCategoryAsync(id));
    }
}
