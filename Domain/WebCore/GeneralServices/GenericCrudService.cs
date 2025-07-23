using System.Net;
using AutoMapper;
using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects;
using Entity.Exceptions;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;
using WebCore.Models;

namespace WebCore.GeneralServices;

public class GenericCrudService<TIn, TOut, TId>(GenericRepository<TIn, TId> repasitory,
    IMapper mapper)
    where TIn : ModelBase<TId>
    where TOut : BaseDto<TId>
{
    public async Task<ResponseModel<TOut>> OnSaveAsync(TOut model)
    {
        if (EqualityComparer<TId>.Default.Equals(model.Id, default))
            return ResponseModel<TOut>.ResultFromContent(
                mapper.Map<TOut>(
                    await repasitory.AddWithSaveChangesAsync(
                        mapper.Map<TIn>(model))),HttpStatusCode.Created);

        var entity = await repasitory.GetByIdAsync(model.Id) ?? throw new NotFoundException($"Not found {typeof(TIn).Name}");
        mapper.Map(model, entity);
        await repasitory.UpdateWithSaveChangesAsync(entity);

        return ResponseModel<TOut>.ResultFromContent(mapper.Map<TOut>(entity));
    }
    public async Task<ResponseModel<List<TOut>>> GetAllAsync(MetaQueryModel metaQuery)
    {
        var query = repasitory.GetAllAsQueryable()
            .FilterByExpressions(metaQuery);

        var items = await query
            .Sort(metaQuery)
            .Paging(metaQuery)
            .Select(c => mapper.Map<TOut>(c))
            .ToListAsync();

        var totalCount = await query.CountAsync();

        return ResponseModel<List<TOut>>.ResultFromContent(
            items,
            total: totalCount);
    }
    public async Task<ResponseModel<TOut>> GetByIdAsync(TId id)
    {
        var item = await repasitory.GetByIdAsync(id) ??
                   throw new NotFoundException($"Not found {typeof(TIn).Name}");
        
        return ResponseModel<TOut>.ResultFromContent(
            mapper.Map<TOut>(item));
    }
    public async Task<ResponseModel<TOut>> DeleteByIdAsync(TId id)
    {
        var item = await repasitory.RemoveWithSaveChangesAsync(id) ??
                   throw new NotFoundException($"Not found {typeof(TIn).Name}");
        
        return ResponseModel<TOut>.ResultFromContent(
            mapper.Map<TOut>(item));
    }
}