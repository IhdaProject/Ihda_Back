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
public class ArticleController : ApiControllerBase
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpPost]
    public async ValueTask<ResponseModel> CreateArticle(ArticleDto article)
    {
        return ResponseModel
            .ResultFromContent(await _articleService.CreateArticleAsync(article));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetAllArticle([FromQuery] MetaQueryModel metaQuery)
    {
        if (Request.Query.Count == 0)
            metaQuery.Take = 1000;

        return ResponseModel

            .ResultFromContent(await _articleService.GetAllArticleAsync(metaQuery));
    }
    
    [HttpGet]
    public async ValueTask<ResponseModel> GetAllArticleByCategoryId([FromQuery] MetaQueryModel metaQuery,[FromQuery]int categoryId)
    {
        if (Request.Query.Count == 0)
            metaQuery.Take = 1000;

        return ResponseModel

            .ResultFromContent(await _articleService.GetAllArticleByCategoryIdAsync(metaQuery,categoryId));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetArticleById(int id)
    {
        return ResponseModel
            .ResultFromContent(await _articleService.GetArticleByIdAsync(id));
    }
    [HttpGet]
    public async ValueTask<ResponseModel> GetArticleWithDetails([FromQuery] MetaQueryModel metaQuery)
    {
        return ResponseModel
            .ResultFromContent(await _articleService.GetArticleWithDetailsAsync(metaQuery));
    }

    [HttpPut]
    public async ValueTask<ResponseModel> UpdateArticle(Article article)
    {
        return ResponseModel
            .ResultFromContent(await _articleService.UpdateArticleAsync(article));
    }

    [HttpDelete]
    public async ValueTask<ResponseModel> DeleteArticle(int id)
    {
        return ResponseModel
            .ResultFromContent(await _articleService.DeleteArticleAsync(id));
    }
}