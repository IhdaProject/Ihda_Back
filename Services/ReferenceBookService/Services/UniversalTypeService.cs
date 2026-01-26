using DatabaseBroker.Extensions;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Exceptions;
using Entity.Models.ApiModels;
using Entity.Models.ReferenceBook;
using Microsoft.EntityFrameworkCore;

namespace ReferenceBookService.Services;

public class UniversalTypeService(GenericRepository<TypeSchema, long> typeRepository) : IUniversalTypeService
{
    public async Task<ResponseModel<TypeSchemaDto>> GetTypeSchemaByNameAsync(string name)
    {
        var type = await typeRepository.GetAllAsQueryable()
            .Where(x => x.Name == name)
            .FirstOrDefaultAsync() ?? throw new NotFoundException("Type not found");

        return new ResponseModel<TypeSchemaDto>()
        {
            Content = new TypeSchemaDto(
                type.Id,
                type.Name,
                type.Description,
                [
                    ..type.Fields.Select(tf =>
                        new FieldSchemaDto(
                            tf.Name,
                            tf.Type,
                            tf.TypeId,
                            tf.Required,
                            tf.Unique,
                            tf.IsArray,
                            tf.Min,
                            tf.Max,
                            tf.MinLength,
                            tf.MaxLength,
                            tf.Pattern))
                ])
        };
    }
    public async Task<ResponseModel<TypeSchemaDto>> GetTypeSchemaByIdAsync(long id)
    {
        var type = await typeRepository.GetByIdAsync(id) ??
                   throw new NotFoundException("Type not found");

        return new ResponseModel<TypeSchemaDto>()
        {
            Content = new TypeSchemaDto(
                type.Id,
                type.Name,
                type.Description,
                [
                    ..type.Fields.Select(tf =>
                        new FieldSchemaDto(
                            tf.Name,
                            tf.Type,
                            tf.TypeId,
                            tf.Required,
                            tf.Unique,
                            tf.IsArray,
                            tf.Min,
                            tf.Max,
                            tf.MinLength,
                            tf.MaxLength,
                            tf.Pattern))
                ])
        };
    }
    public async Task<ResponseModel<List<TypeSchemaDto>>> GetTypeSchemasAsync(MetaQueryModel metaQueryModel)
    {
        var query = typeRepository.GetAllAsQueryable()
            .FilterByExpressions(metaQueryModel);

        return new ResponseModel<List<TypeSchemaDto>>()
        {
            Total = await query.CountAsync(),
            Content = await query
                .Paging(metaQueryModel)
                .Select(t => new TypeSchemaDto(
                    t.Id,
                    t.Name,
                    t.Description,
                    new List<FieldSchemaDto>(0)))
                .ToListAsync()
        };
    }
    public Task<ResponseModel<bool>> CreateTypeAsync(ChangeTypeDto typeSchema)
    {
        throw new NotImplementedException();
    }
    public Task<ResponseModel<ResultUpdateTypeDto>> UpdateTypeAsync(ChangeTypeDto typeSchema)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<ResultUpdateTypeDto>> CheckUpdateTypeAsync(ChangeTypeDto typeSchema)
    {
        throw new NotImplementedException();
    }
}