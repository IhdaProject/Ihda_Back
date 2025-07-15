using Entity.DataTransferObjects.Mosques;
using Entity.DataTransferObjects.PrayerTimes;
using Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using MosqueService.Services;
using WebCore.Attributes;
using WebCore.Controllers;
using WebCore.Models;

namespace MosqueApi.Controllers;

public class MosqueController(IMosqueService mosqueService) : ApiControllerBase
{
    [HttpPost("{id}"), PermissionAuthorize(UserPermissions.OnSaveMosque)]
    [ApiGroup("Client","Admin")]
    public async Task<ResponseModel<MosqueDto>> OnSaveMosque([FromBody]MosqueDto mosqueDto, [FromRoute]long id)
        => await mosqueService.OnSaveMosqueAsync(mosqueDto, id, UserId);
    
    [HttpPost("{id}"), PermissionAuthorize(UserPermissions.OnSavePreyerTime)]
    [ApiGroup("Client","Admin")]
    public async Task<ResponseModel<MosquePrayerTimeDto>> OnSaveMosquePreyerTime([FromBody]MosquePrayerTimeDto prayerTimeDto,  [FromRoute]long id)
        => await mosqueService.OnSavePrayerTimeAsync(prayerTimeDto, id, UserId);
    
    [HttpGet, PermissionAuthorize(UserPermissions.ViewMosques)]
    [ApiGroup("Client")]
    public async Task<ResponseModel<List<MosqueByListDto>>> Get([FromQuery]MetaQueryModel metaQuery)
        => await mosqueService.GetListAsync(metaQuery);
    [HttpGet, PermissionAuthorize(UserPermissions.ViewMyMosques)]
    [ApiGroup("Admin")]
    public async Task<ResponseModel<List<MosqueByListDto>>> GetMyMosques([FromQuery]MetaQueryModel metaQuery)
        => await mosqueService.GetMyListAsync(metaQuery, UserId);
    [HttpGet("{id}"), PermissionAuthorize(UserPermissions.ViewMosque)]
    [ApiGroup("Client", "Admin")]
    public async Task<ResponseModel<MosqueWithTimeDto>> GetById(int id)
        => await mosqueService.GetByIdAsync(id);
    [HttpGet, PermissionAuthorize(UserPermissions.ViewFavoritesMosque)]
    [ApiGroup("Client")]
    public async Task<ResponseModel<List<MosqueByListDto>>> GetFavorites([FromQuery]MetaQueryModel metaQuery)
        => await mosqueService.GetFavoriteListAsync(metaQuery, UserId);
    [HttpGet, PermissionAuthorize(UserPermissions.ToggleFavoriteMosque)]
    [ApiGroup("Client")]
    public async Task<ResponseModel<bool>> ToggleFavorite(int id)
        => await mosqueService.ToggleFavoriteAsync(id, UserId);
}