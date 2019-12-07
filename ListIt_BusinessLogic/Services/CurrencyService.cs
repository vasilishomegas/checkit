﻿using System;
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
    public class CurrencyService : Service<Currency, CurrencyDto>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICurrencyConverter _currencyConverter;

        public CurrencyService() : this(new CurrencyRepository(), new CurrencyConverter())
        {

        }

        public CurrencyService(ICurrencyRepository currencyRepository, ICurrencyConverter currencyConverter) : base(currencyRepository, currencyConverter)
        {
            _currencyRepository = currencyRepository;
            _currencyConverter = currencyConverter;
        }
    }
}