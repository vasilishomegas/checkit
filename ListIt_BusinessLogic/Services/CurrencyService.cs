using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class CurrencyService : Service<Currency, CurrencyDto>
    {
        private readonly CurrencyRepository _currencyRepository;
        private readonly CurrencyConverter _currencyConverter;

        public CurrencyService() : base(new CurrencyRepository(), new CurrencyConverter())
        {
            _currencyRepository = (CurrencyRepository) _repository;
            _currencyConverter = (CurrencyConverter) _converter;
        }
    }
}
