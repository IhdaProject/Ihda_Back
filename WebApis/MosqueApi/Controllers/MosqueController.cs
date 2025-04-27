using Entity.DataTransferObjects.Mosques;
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
    public async Task<ResponseModel<MosqueDto>> OnSave([FromBody]MosqueDto mosqueDto)
        => await mosqueService.OnSaveAsync(mosqueDto);
    
    [HttpGet]
    public async Task<ResponseModel<List<MosqueByListDto>>> Get([FromQuery]MetaQueryModel metaQuery)
        => await mosqueService.GetListAsync(metaQuery);

    [HttpGet]
    public async Task<ResponseModel<MosqueDto>> GetById(int id)
        => await mosqueService.GetByIdAsync(id);
    
    [HttpGet]
    public async Task<ResponseModel<List<MosqueByListDto>>> GetFavorites([FromQuery]MetaQueryModel metaQuery)
        => await mosqueService.GetFavoriteListAsync(metaQuery);
    
    [HttpGet]
    public async Task<ResponseModel<bool>> ToggleFavorite(int id)
        => await mosqueService.ToggleFavoriteAsync(id, UserId);
}