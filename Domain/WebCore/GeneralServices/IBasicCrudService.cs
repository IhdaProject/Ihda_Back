using Entity.DataTransferObjects;
using Entity.Models.ApiModels;
using Entity.Models.Common;
using WebCore.Models;

namespace WebCore.GeneralServices;

public interface IBasicCrudService<TIn, TOut, TId>
    where TIn : ModelBase<TId>
    where TOut : BaseDto<TId>
{
    Task<ResponseModel<TOut>> OnSaveAsync(TOut model);
    Task<ResponseModel<List<TOut>>> GetAllAsync(MetaQueryModel metaQuery);
    Task<ResponseModel<TOut>> GetByIdAsync(TId id);
}