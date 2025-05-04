using Entity.DataTransferObjects.Mosques;
using Entity.DataTransferObjects.PrayerTimes;
using Entity.Models.ApiModels;
using WebCore.Models;

namespace MosqueService.Services;

public interface IMosqueService
{
    Task<ResponseModel<List<MosqueByListDto>>> GetListAsync(MetaQueryModel queryModel);
    Task<ResponseModel<List<MosqueByListDto>>> GetFavoriteListAsync(MetaQueryModel queryModel, long userId);
    Task<ResponseModel<MosqueWithTimeDto>> GetByIdAsync(long id);
    Task<ResponseModel<MosqueDto>> OnSaveMosqueAsync(MosqueDto mosqueDto, long userId);
    Task<ResponseModel<MosquePrayerTimeDto>> OnSavePrayerTimeAsync(MosquePrayerTimeDto mosqueDto, long userId);
    Task<ResponseModel<bool>> ToggleFavoriteAsync(long id, long userId);
}