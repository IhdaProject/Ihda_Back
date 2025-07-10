using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;
using WebCore.Controllers;
using WebCore.GeneralServices;

namespace ReferenceBookApi.Controllers;

public class CountryController(GenericCrudService<Country, CountryDto, long> crudService) : CrudGenericController<Country, CountryDto, long>(crudService)
{
}