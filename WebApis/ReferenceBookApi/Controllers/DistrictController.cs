using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;
using WebCore.Controllers;
using WebCore.GeneralServices;

namespace ReferenceBookApi.Controllers;

public class DistrictController(GenericCrudService<District, DistrictDto, long> crudService) : CrudGenericController<District, DistrictDto, long>(crudService)
{
}