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
    public class UnitTypeService : Service<UnitType, UnitTypeDto>
    {
        private readonly UnitTypesRepository _unitTypeRepository;
        private readonly UnitTypeConverter _unitTypeConverter;

        public UnitTypeService() : base(new UnitTypesRepository(), new UnitTypeConverter())
        {
            _unitTypeRepository = (UnitTypesRepository)_repository;
            _unitTypeConverter = (UnitTypeConverter) _converter;
        }

        public IList<UnitTypeDto> GetUnitTypesByLanguage(int langId)
        {
            var unitList = _unitTypeRepository.GetUnitTypesByLanguage(langId);
            List<UnitTypeDto> unitTypes = new List<UnitTypeDto>();

            foreach(TranslationOfUnitType translation in unitList)
            {
                var unit = ConvertDBToDto(translation);
                unitTypes.Add(unit);
            }

            return unitTypes;
        }

        protected UnitTypeDto ConvertDBToDto(TranslationOfUnitType translation)
        {
            return StaticDBtranslationToDto(translation);
        }

        public static UnitTypeDto StaticDBtranslationToDto(TranslationOfUnitType translation)
        {
            if (translation == null) return null;
            return new UnitTypeDto
            {
                Id = translation.UnitType_Id,
                Name = translation.Translation,
                LanguageId =  translation.Language_Id
            };
        }
    }
}

