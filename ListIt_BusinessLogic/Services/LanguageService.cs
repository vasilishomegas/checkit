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
    public class LanguageService : Service<Language, LanguageDto>
    {
        public LanguageService() : base(new LanguageRepository())
        {

        }

        public override IEnumerable<LanguageDto> GetAll()
        {
            return _repository.GetAll().Select(ConvertDamToDto).ToList();
        } 

        protected override LanguageDto ConvertDamToDto(Language entity)
        {
            return StaticDamToDto(entity);
        }

        protected override Language ConvertDtoToDam(LanguageDto dto)
        {
            return StaticDtoToDam(dto);
        }

        public static Language StaticDtoToDam(LanguageDto languageDto)
        {
            if (languageDto == null) return null;
            return new Language
            {
                Code = languageDto.Code,
                Id = languageDto.Id,
                Name = languageDto.Name
            };
        }

        public static LanguageDto StaticDamToDto(Language language)
        {
            if (language == null) return null;
            return new LanguageDto
            {
                Code = language.Code,
                Id = language.Id,
                Name = language.Name
            };
        }
    }
}
