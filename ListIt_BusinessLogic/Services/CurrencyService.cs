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
    public class CurrencyService : Service<Currency, CurrencyDto>
    {
        private readonly CurrencyRepository _currRepository;
        public CurrencyService() : base(new CurrencyRepository())
        {
            _currRepository = (CurrencyRepository)_repository;
        }

        public override IEnumerable<CurrencyDto> GetAll()
        {
            return _repository.GetAll().Select(ConvertDBToDto).ToList();
        }

        protected override CurrencyDto ConvertDBToDto(Currency entity)
        {
            return StaticDBToDto(entity);
        }

        protected override Currency ConvertDtoToDB(CurrencyDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static Currency StaticDtoToDB(CurrencyDto languageDto)
        {
            if (languageDto == null) return null;
            return new Currency
            {
                Id = languageDto.Id,
                Name = languageDto.Name,
                Sign = languageDto.Sign,
                Code = languageDto.Code
            };
        }

        public static CurrencyDto StaticDBToDto(Currency language)
        {
            if (language == null) return null;
            return new CurrencyDto
            {
                Id = language.Id,
                Name = language.Name,
                Sign = language.Sign,
                Code = language.Code
            };
        }
    }
}
