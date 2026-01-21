using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ApiModels;

namespace ReferenceBookService.Services;

public class UniversalTypeService() : IUniversalTypeService
{
    public Task<ResponseModel<TypeSchemaDto>> GetTypeSchemaByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<TypeSchemaDto>> GetTypeSchemaByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<List<TypeSchemaDto>>> GetTypeSchemasAsync(MetaQueryModel metaQueryModel)
    {
        throw new NotImplementedException();
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