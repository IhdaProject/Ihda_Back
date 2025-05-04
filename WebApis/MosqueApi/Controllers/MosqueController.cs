using Entity.DataTransferObjects.Mosques;
using Entity.DataTransferObjects.PrayerTimes;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using MosqueService.Services;
using WebCore.Controllers;
using WebCore.Models;

namespace MosqueApi.Controllers;

[ApiExplorerSettings(GroupName = "Client")]
public class MosqueController(IMosqueService mosqueService) : ApiControllerBase
{
    [HttpPost]
    [ApiExplorerSettings(GroupName = "Admin")]
    public async Task<ResponseModel<MosqueDto>> OnSaveMosque([FromBody]MosqueDto mosqueDto)
        => await mosqueService.OnSaveMosqueAsync(mosqueDto, UserId);
    
    [HttpPost]
    [ApiExplorerSettings(GroupName = "Admin")]
    public async Task<ResponseModel<MosquePrayerTimeDto>> OnSaveMosquePreyerTime([FromBody]MosquePrayerTimeDto prayerTimeDto)
        => await mosqueService.OnSavePrayerTimeAsync(prayerTimeDto, UserId);
    
    [HttpGet]
    public async Task<ResponseModel<List<MosqueByListDto>>> Get([FromQuery]MetaQueryModel metaQuery)
        => await mosqueService.GetListAsync(metaQuery);

    [HttpGet]
    public async Task<ResponseModel<MosqueWithTimeDto>> GetById(int id)
        => await mosqueService.GetByIdAsync(id);
    
    [HttpGet]
    public async Task<ResponseModel<List<MosqueByListDto>>> GetFavorites([FromQuery]MetaQueryModel metaQuery)
        => await mosqueService.GetFavoriteListAsync(metaQuery, UserId);
    
    [HttpGet]
    public async Task<ResponseModel<bool>> ToggleFavorite(int id)
        => await mosqueService.ToggleFavoriteAsync(id, UserId);
}