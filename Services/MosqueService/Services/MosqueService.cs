using DatabaseBroker.Repositories.Mosques;
using Entity.DataTransferObjects.Mosques;
using Entity.Models.ApiModels;
using WebCore.Models;

namespace MosqueService.Services;

public class MosqueService(IMosqueRepository mosqueRepository) : IMosqueService
{
    public Task<ResponseModel<List<MosqueByListDto>>> GetListAsync(MetaQueryModel queryModel)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<List<MosqueByListDto>>> GetFavoriteListAsync(MetaQueryModel queryModel)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<MosqueDto>> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<bool>> ToggleFavoriteAsync(long id, long userId)
    {
        throw new NotImplementedException();
    }
}