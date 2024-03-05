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
public class HashtagController : ApiControllerBase
{
    private readonly IHashtagService _hashtagService;

    public HashtagController(IHashtagService hashtagService)
    {
        _hashtagService = hashtagService;
    }

    [HttpPost]
    public async ValueTask<ResponseModel> CreateHashTag(HashtagDto hashtagDto)
    {
        return ResponseModel
            .ResultFromContent(await _hashtagService.CreateHashtagAsync(hashtagDto));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetAllHashTag([FromQuery] MetaQueryModel metaQuery)
    {
        if (Request.Query.Count == 0)
            metaQuery.Take = 1000;

        return ResponseModel
            .ResultFromContent(await _hashtagService.GetAllHashtagAsync(metaQuery));
    }
    [HttpGet]
    public async ValueTask<ResponseModel> GetHashTagById(int id)
    {
        return ResponseModel
            .ResultFromContent(await _hashtagService.GetHashtagByIdAsync(id));
    }

    [HttpGet]
    public async ValueTask<ResponseModel> GetAllByHashTagId([FromQuery] MetaQueryModel metaQuery,int id,string category)
    {
        return ResponseModel
            .ResultFromContent(await _hashtagService.GetAllByHashtagId(id,category,metaQuery));
    }

    [HttpPut]
    public async ValueTask<ResponseModel> UpdateHashTag(Hashtag hashtag)
    {
        return ResponseModel
            .ResultFromContent(await _hashtagService.UpdateHashtagAsync(hashtag));
    }

    [HttpDelete]
    public async ValueTask<ResponseModel> DeleteHashTag(int id)
    {
        return ResponseModel
            .ResultFromContent(await _hashtagService.DeleteHashtagAsync(id));
    }
}
