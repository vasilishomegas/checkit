using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class CountryConverter : ICountryConverter
    {
        public CountryDto ConvertDBToDto(Country country)
        {
            if (country == null) return null;
            return new CountryDto
            {
                Code = country.Code,
                Id = country.Id,
                Name = country.Name
            };
        }

        public Country ConvertDtoToDB(CountryDto countryDto)
        {
            if (countryDto == null) return null;
            return new Country
            {
                Code = countryDto.Code,
                Id = countryDto.Id,
                Name = countryDto.Name
            };
        }
    }
}