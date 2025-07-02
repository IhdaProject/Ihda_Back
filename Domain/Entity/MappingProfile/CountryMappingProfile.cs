using AutoMapper;
using Entity.DataTransferObjects.ReferenceBook;
using Entity.Models.ReferenceBook;

namespace Entity.MappingProfile;

public class CountryMappingProfile : Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Country, CountryDto>();
        CreateMap<CountryDto, Country>();
    }
}