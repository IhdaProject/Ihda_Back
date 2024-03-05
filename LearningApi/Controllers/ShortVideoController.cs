using Entity.DataTransferObjects.Learning;
using Entity.Models.ApiModels;
using Entity.Models.Learning;
using LearningService.Service;
using Microsoft.AspNetCore.Mvc;
using WebCore.Controllers;
using WebCore.Models;

namespace LearningApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShortVideoController : ApiControllerBase
    {
        private readonly IShortVideoService _shortVideoService;

        public ShortVideoController(IShortVideoService shortVideoService)
        {
            _shortVideoService = shortVideoService;
        }

        [HttpPost]
        public async ValueTask<ResponseModel> CreateShortVideo(ShortVideoDto shortVideoDto)
        {
            return ResponseModel
                .ResultFromContent(await _shortVideoService.CreateShortVideoAsync(shortVideoDto));
        }

        [HttpGet]
        public async ValueTask<ResponseModel> GetAllShortVideo([FromQuery] MetaQueryModel metaQuery)
        {
            if (Request.Query.Count == 0)
                metaQuery.Take = 1000;

            return ResponseModel
                .ResultFromContent(await _shortVideoService.GetAllShortVideoAsync(metaQuery));
        }
        
        [HttpGet]
        public async ValueTask<ResponseModel> GetAllShortVideoWithDetails([FromQuery] MetaQueryModel metaQuery)
        {
            if (Request.Query.Count == 0)
                metaQuery.Take = 1000;

            return ResponseModel
                .ResultFromContent(await _shortVideoService.GetShortVideoWithDetailsAsync(metaQuery));
        }
        
        [HttpGet]
        public async ValueTask<ResponseModel> GetShortVideoCategoryId([FromQuery] int categoryId,[FromQuery] MetaQueryModel metaQuery)
        {
            if (Request.Query.Count == 1)
                metaQuery.Take = 1000;

            return ResponseModel
                .ResultFromContent(await _shortVideoService.GetShortVideoByCategoryIdAsync(metaQuery,categoryId));
        }
        [HttpGet]
        public async ValueTask<ResponseModel> GetShortVideoByAuthorId([FromQuery] int authorId,[FromQuery] MetaQueryModel metaQuery)
        {
            if (Request.Query.Count == 1)
                metaQuery.Take = 1000;

            return ResponseModel
                .ResultFromContent(await _shortVideoService.GetShortVideoByAftorIdAsync(metaQuery,authorId));
        }

        [HttpGet]
        public async ValueTask<ResponseModel> GetShortVideoByHashtagId([FromQuery]  int id,[FromQuery] MetaQueryModel metaQuery)
        {
            if (Request.Query.Count == 1)
                metaQuery.Take = 1000;

            return ResponseModel
                .ResultFromContent(await _shortVideoService.GetShortVideoByHashtagIdAsync(metaQuery,id));
        }

        [HttpPut]
        public async ValueTask<ResponseModel> UpdateShortVideo(ShortVideo shortVideo)
        {
            return ResponseModel
                .ResultFromContent(await _shortVideoService.UpdateShortVideoAsync(shortVideo));
        }

        [HttpDelete]
        public async ValueTask<ResponseModel> DeleteShortVideo(int id)
        {
            return ResponseModel
                .ResultFromContent(await _shortVideoService.DeleteShortVideoAsync(id));
        }
 
    }
}
