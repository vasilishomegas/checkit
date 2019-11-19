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

        protected override LanguageDto ConvertDomainToDto(Language entity)
        {
            return StaticDomainToDto(entity);
        }

        protected override Language ConvertDtoToDomain(LanguageDto dto)
        {
            return StaticDtoToDomain(dto);
        }

        public static Language StaticDtoToDomain(LanguageDto languageDto)
        {
            if (languageDto == null) return null;
            return new Language
            {
                Code = languageDto.Code,
                Id = languageDto.Id,
                Name = languageDto.Name
            };
        }

        public static LanguageDto StaticDomainToDto(Language language)
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
