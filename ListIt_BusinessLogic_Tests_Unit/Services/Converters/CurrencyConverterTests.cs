using System;
using System.Collections.Generic;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ListIt_BusinessLogic_Tests_Unit.Services.Converters
{ 
    public class CurrencyConverterTests
    {
        [Test]
        public void ConvertDbToDto_NullShouldReturnNull()
        {
            Currency currency = null;
            var converter = new CurrencyConverter();

            var currencyDto = converter.ConvertDBToDto(currency);

            Assert.IsNull(currencyDto);
        }

        [Test]
        public void ConvertDtoToDb_NullShouldReturnNull()
        {
            CurrencyDto currencyDto = null;
            var converter = new CurrencyConverter();

            var currency = converter.ConvertDtoToDB(currencyDto);

            Assert.IsNull(currency);
        }

        [Test]
        public void ConvertDbToDto_ReturnsConvertedObject()
        {
            var currency = new Currency
            {
                Id = 1,
                Name = "Sample Name",
                Users = new List<User>() { new User { Id = 1, Nickname = "Sample Nickname" } },
                Code = "Sample Code",
                ApiProducts = new List<ApiProduct>() { new ApiProduct { Id = 1 } },
                DefaultProducts = new List<DefaultProduct>() { new DefaultProduct() },
                Sign = "Sample Sign",
                UserProducts = new List<UserProduct>() { new UserProduct() }
            };
            var converter = new CurrencyConverter();

            var currencyDto = converter.ConvertDBToDto(currency);

            Assert.AreEqual(currencyDto.Id, currency.Id);
            Assert.AreEqual(currencyDto.Code, currency.Code);
            Assert.AreEqual(currencyDto.Name, currency.Name);
            Assert.AreEqual(currencyDto.Sign, currency.Sign);
        }

        [Test]
        public void ConvertDtoToDb_ReturnsConvertedObject()
        {
            var currencyDto = new CurrencyDto
            {
                Id = 1,
                Name = "Sample Name",
                Code = "Sample Code",
                Sign = "Sample Sign"
            };
            var converter = new CurrencyConverter();

            var currency = converter.ConvertDtoToDB(currencyDto);

            Assert.AreEqual(currencyDto.Id, currency.Id);
            Assert.AreEqual(currencyDto.Code, currency.Code);
            Assert.AreEqual(currencyDto.Name, currency.Name);
            Assert.AreEqual(currencyDto.Sign, currency.Sign);
            Assert.IsNotNull(currency.ApiProducts);
            Assert.IsNotNull(currency.DefaultProducts);
            Assert.IsNotNull(currency.UserProducts);
            Assert.IsNotNull(currency.Users);
        }
    }
}
