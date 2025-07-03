using System.Net;
using AutoMapper;
using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects;
using Entity.Exeptions;
using Entity.Models.ApiModels;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;
using WebCore.Models;

namespace WebCore.GeneralServices;

public class BasicCrudService<TIn, TOut, TId>(GenericRepository<TIn, TId> repasitory,
    IMapper mapper) : IBasicCrudService<TIn, TOut, TId>
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

        var entity = await repasitory.GetByIdAsync(model.Id) ?? throw new NotFoundException($"Not found {nameof(TIn)}");
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
        => ResponseModel<TOut>.ResultFromContent(
            mapper.Map<TOut>(await repasitory.GetByIdAsync(id)));
}