using AutoMapper;
using DatabaseBroker.Repositories;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;
using WebCore.GeneralServices;

namespace ReferenceBookService.Services;

public class CountryService(GenericRepository<Country, long> countryRepository,
    IMapper mapper) : BasicCrudService<Country, CountryDto, long>(countryRepository, mapper), ICountryService
{
    
}