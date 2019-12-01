using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class CountryService : Service<Country, CountryDto>
    {
        public CountryService() : base(new CountryRepository())
        {

        }

        protected override CountryDto ConvertDamToDto(Country entity)
        {
            return StaticDamToDto(entity);
        }

        protected override Country ConvertDtoToDam(CountryDto dto)
        {
            return StaticDtoToDam(dto);
        }

        public static Country StaticDtoToDam(CountryDto countryDto)
        {
            if (countryDto == null) return null;
            return new Country
            {
                Code = countryDto.Code,
                Id = countryDto.Id,
                Name = countryDto.Name
            };
        }

        public static CountryDto StaticDamToDto(Country country)
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

