using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;
using WebCore.GeneralServices;

namespace ReferenceBookService.Services;

public interface ICountryService : IBasicCrudService<Country, CountryDto, long>
{
    
}