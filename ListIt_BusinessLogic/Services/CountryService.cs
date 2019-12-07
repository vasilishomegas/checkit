using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainInterface.Interfaces.Repository;
using ListIt_DomainModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class CountryService : Service<Country, CountryDto>
    {
        public CountryService(): this(new CountryRepository(), new CountryConverter())
        {

        }

        public CountryService(ICountryRepository countryRepository, ICountryConverter countryConverter) : base(countryRepository, countryConverter)
        {

        }
    }
}

