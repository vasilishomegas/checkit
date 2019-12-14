using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_BusinessLogic.Services.Interface;
using ListIt_DataAccess.Repository;
using ListIt_DataAccess.Repository.Interface;
using ListIt_DataAccessModel;
using ListIt_DomainModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class UnitTypeService : Service<UnitType, UnitTypeDto>, IUnitTypeService
    {
        private readonly IUnitTypeRepository _unitTypeRepository;
        private readonly IUnitTypeConverter _unitTypeConverter;

        public UnitTypeService(): this(new UnitTypeRepository(), new UnitTypeConverter())
        {

        }

        public UnitTypeService(IUnitTypeRepository unitTypeRepository, IUnitTypeConverter unitTypeConverter) : base(unitTypeRepository, unitTypeConverter)
        {
            _unitTypeConverter = unitTypeConverter;
            _unitTypeRepository = unitTypeRepository;
        }

        public IList<UnitTypeDto> GetUnitTypesByLanguage(int langId)
        {
            var unitList = _unitTypeRepository.GetUnitTypesByLanguage(langId);

            return unitList.Select(ConvertDBToDto).ToList();
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
                // Name = translation.Translation,
                // LanguageId =  translation.Language_Id
                // TODO
            };
        }
    }
}

