using ListIt_BusinessLogic.Services;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
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
