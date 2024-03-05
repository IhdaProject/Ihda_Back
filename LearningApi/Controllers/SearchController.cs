using Entity.Models.ApiModels;
using LearningService.Service;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace LearningApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SearchController : ApiControllerBase
{
    private readonly IArticleService _articleService;
    private readonly ICourseService _courseService;
    private readonly IShortVideoService _shortVideoService;
    private readonly ISeminarVideoService _seminarVideoService;
    public SearchController(IArticleService articleService, 
                    ICourseService courseService, 
                    IShortVideoService shortVideoService, 
                    ISeminarVideoService seminarVideoService)
    {
        _articleService = articleService;
        _courseService = courseService;
        _shortVideoService = shortVideoService;
        _seminarVideoService = seminarVideoService;
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetShortVideoCategoryId([FromQuery] int categoryId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _shortVideoService.GetShortVideoByCategoryIdAsync(metaQuery, categoryId));
    }
    [HttpGet]
    public async ValueTask<ResponseModel> GetShortVideoByAuthorId([FromQuery] int authorId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _shortVideoService.GetShortVideoByAftorIdAsync(metaQuery, authorId));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetShortVideoByHashtagId([FromQuery] int id, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _shortVideoService.GetShortVideoByHashtagIdAsync(metaQuery, id));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetArticleCategoryId([FromQuery] int categoryId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _articleService.GetAllArticleByCategoryIdAsync(metaQuery, categoryId));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetArticleByAuthorId([FromQuery] int authorId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _articleService.GetAllArticleByAuthorIdAsync(metaQuery, authorId));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetArticleByHashtagId([FromQuery] int hashtagId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _articleService.GetAllArticleByHashtagIdAsync(metaQuery, hashtagId));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetCourseCategoryId([FromQuery] int categoryId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _courseService.GetAllCourseByCategoryIdAsync(metaQuery, categoryId)); 
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetCourseByAuthorId([FromQuery] int authorId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _courseService.GetAllCourseByAuthorIdAsync(metaQuery, authorId));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetCourseByHashtagId([FromQuery] int hashtagId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _courseService.GetAllCourseByHashtagIdAsync(metaQuery, hashtagId));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetSeminarVideoCategoryId([FromQuery] int categoryId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.GetSeminarVideoByCategoryIdAsync(metaQuery, categoryId));
    }
    [HttpGet]
    public async ValueTask<ResponseModel> GetSeminarVideoByAuthorId([FromQuery] int authorId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.GetAllSeminarVideoByAuthorIdAsync(metaQuery, authorId));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetSeminarVideoByHashtagId([FromQuery] int hashtagId, [FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _seminarVideoService.GetAllSeminarVideoByHashtagIdAsync(metaQuery, hashtagId));
    }
}
