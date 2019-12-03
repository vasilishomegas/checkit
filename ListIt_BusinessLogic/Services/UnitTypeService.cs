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
    public class UnitTypeService : Service<UnitType, UnitTypesDto>
    {
        private readonly UnitTypesRepository _unitRepository;
        public UnitTypeService() : base(new UnitTypesRepository())
        {
            _unitRepository = (UnitTypesRepository)_repository;
        }

        public IList<UnitTypesDto> GetUnitTypesByLanguage(int langId)
        {
            var unitList = _unitRepository.GetUnitTypesByLanguage(langId);
            List<UnitTypesDto> unitTypes = new List<UnitTypesDto>();

            foreach(TranslationOfUnitType translation in unitList)
            {
                var unit = ConvertDBToDto(translation);
                unitTypes.Add(unit);
            }

            return unitTypes;
        }

        protected UnitTypesDto ConvertDBToDto(TranslationOfUnitType translation)
        {
            return StaticDBtranslationToDto(translation);
        }

        protected override UnitTypesDto ConvertDBToDto(UnitType entity)
        {
            return StaticDBToDto(entity);
        }

        protected override UnitType ConvertDtoToDB(UnitTypesDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static UnitType StaticDtoToDB(UnitTypesDto dto)
        {
            if (dto == null) return null;
            return new UnitType
            {
                Id = dto.Id
            };
        }

        public static UnitTypesDto StaticDBtranslationToDto(TranslationOfUnitType translation)
        {
            if (translation == null) return null;
            return new UnitTypesDto
            {
                Id = translation.UnitType_Id,
                Name = translation.Translation,
                LanguageId =  translation.Language_Id
            };
        }

        public static UnitTypesDto StaticDBToDto(UnitType unit)
        {
            return new UnitTypesDto
            {
                Id = unit.Id
            };
        }
    }
}

