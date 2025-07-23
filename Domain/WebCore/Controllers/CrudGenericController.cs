using Microsoft.AspNetCore.Mvc;
using WebCore.Attributes;
using WebCore.GeneralServices;
using WebCore.Models;

namespace WebCore.Controllers;

public class CrudGenericController<TIn, TOut, TId>(GenericCrudService<TIn, TOut, TId> crudService) : ApiControllerBase
    where TIn : Entity.Models.Common.ModelBase<TId>
    where TOut : Entity.DataTransferObjects.BaseDto<TId>
{
    [HttpGet]
    [ApiGroup("Client", "Admin")]
    public Task<ResponseModel<List<TOut>>> GetAll([FromQuery]MetaQueryModel metaQuery)
        => crudService.GetAllAsync(metaQuery);
    [HttpGet("{id}")]
    [ApiGroup("Client", "Admin")]
    public Task<ResponseModel<TOut>> GetById([FromRoute]TId id)
        => crudService.GetByIdAsync(id);
    [HttpPost]
    [ApiGroup("Admin")]
    public Task<ResponseModel<TOut>> OnSave([FromBody]TOut item)
        => crudService.OnSaveAsync(item);
    [HttpDelete]
    [ApiGroup("Admin")]
    public Task<ResponseModel<TOut>> Delete([FromBody]TId id)
        => crudService.DeleteByIdAsync(id);
}