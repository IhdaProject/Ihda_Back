using Entity.DataTransferObjects.ReferenceBook;
using Entity.Enums;
using Entity.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using ReferenceBookService.Services;
using WebCore.Attributes;
using WebCore.Controllers;

namespace ReferenceBookApi.Controllers;
[ApiGroup("Admin")]
[Route("api/[controller]")]
public class UniversalTypeController(IUniversalTypeService universalTypeService) : ApiControllerBase
{
    [HttpGet("{name}")]
    [PermissionAuthorize(UserPermissions.UniversalTypeViewWithDetails)]
    public Task<ResponseModel<TypeSchemaDto>> GetTypeSchema([FromRoute] string name)
        => long.TryParse(name, out var id) ?
            universalTypeService.GetTypeSchemaByIdAsync(id) :
            universalTypeService.GetTypeSchemaByNameAsync(name);
    
    [HttpGet]
    [PermissionAuthorize(UserPermissions.UniversalTypesView)]
    public Task<ResponseModel<List<TypeSchemaDto>>> GetTypeSchemas([FromQuery] MetaQueryModel metaQueryModel)
        => universalTypeService.GetTypeSchemasAsync(metaQueryModel);

    [HttpPost]
    [PermissionAuthorize(UserPermissions.UniversalTypeCreate)]
    public Task<ResponseModel<bool>> CreateTypeSchema([FromBody] ChangeTypeDto typeSchema)
        => universalTypeService.CreateTypeAsync(typeSchema);
}