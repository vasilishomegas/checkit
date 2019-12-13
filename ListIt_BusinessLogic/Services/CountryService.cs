using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class CountryService : Service<Country, CountryDto>
    {
        public CountryService() : base(new CountryRepository())
        {

        }

        protected override CountryDto ConvertDBToDto(Country entity)
        {
            return StaticDBToDto(entity);
        }

        protected override Country ConvertDtoToDB(CountryDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static Country StaticDtoToDB(CountryDto countryDto)
        {
            if (countryDto == null) return null;
            return new Country
            {
                Code = countryDto.Code,
                Id = countryDto.Id,
                Name = countryDto.Name
            };
        }

        public static CountryDto StaticDBToDto(Country country)
        {
            if (country == null) return null;
            return new CountryDto
            {
                Code = country.Code,
                Id = country.Id,
                Name = country.Name
            };
        }
    }
}

