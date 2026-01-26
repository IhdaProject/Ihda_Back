using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ApiModels;

namespace ReferenceBookService.Services;

public interface IUniversalTypeService
{
    Task<ResponseModel<TypeSchemaDto>> GetTypeSchemaByNameAsync(string name);
    Task<ResponseModel<TypeSchemaDto>> GetTypeSchemaByIdAsync(long id);
    Task<ResponseModel<List<TypeSchemaDto>>> GetTypeSchemasAsync(MetaQueryModel metaQueryModel);
    Task<ResponseModel<bool>> CreateTypeAsync(ChangeTypeDto typeSchema);
    Task<ResponseModel<ResultUpdateTypeDto>> UpdateTypeAsync(ChangeTypeDto typeSchema);
    Task<ResponseModel<ResultUpdateTypeDto>> CheckUpdateTypeAsync(ChangeTypeDto typeSchema);
}