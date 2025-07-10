using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;
using WebCore.Controllers;
using WebCore.GeneralServices;

namespace ReferenceBookApi.Controllers;

public class RegionController(GenericCrudService<Region, RegionDto, long> crudService) : CrudGenericController<Region, RegionDto, long>(crudService)
{
}