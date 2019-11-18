using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ListIt_BusinessLogic.DTO;
using ListIt_BusinessLogic.Services;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DomainModel;
using ListIt_WebAPI.Controllers.Generics;

namespace ListIt_WebAPI.Controllers
{
    public class CountriesController : GenericController<Country, CountryDto>
    {
        public CountriesController() : base(new CountryService())
        {

        }
    }
}
