using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class CurrencyConverter : ICurrencyConverter
    {
        public CurrencyDto ConvertDBToDto(Currency currency)
        {
            if (currency == null) return null;
            return new CurrencyDto
            {
                Id = currency.Id,
                Name = currency.Name,
                Sign = currency.Sign,
                Code = currency.Code
            };
        }

        public Currency ConvertDtoToDB(CurrencyDto currencyDto)
        {
            if (currencyDto == null) return null;
            return new Currency
            {
                Id = currencyDto.Id,
                Name = currencyDto.Name,
                Sign = currencyDto.Sign,
                Code = currencyDto.Code
            };
        }
    }
}
