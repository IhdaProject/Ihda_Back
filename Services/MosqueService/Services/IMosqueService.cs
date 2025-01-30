using Entity.DataTransferObjects.Mosques;
using Entity.Models.ApiModels;
using WebCore.Models;

namespace MosqueService.Services;

public interface IMosqueService
{
    Task<ResponseModel<List<MosqueByListDto>>> GetListAsync(MetaQueryModel queryModel);
    Task<ResponseModel<List<MosqueByListDto>>> GetFavoriteListAsync(MetaQueryModel queryModel);
    Task<ResponseModel<MosqueDto>> GetByIdAsync(long id);
    Task<ResponseModel<MosqueDto>> OnSaveAsync(MosqueDto mosqueDto);
    Task<ResponseModel<bool>> ToggleFavoriteAsync(long id, long userId);
}