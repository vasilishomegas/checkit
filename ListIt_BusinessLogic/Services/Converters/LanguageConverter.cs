using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class LanguageConverter : ILanguageConverter
    {
        public LanguageDto ConvertDBToDto(Language language)
        {
            if (language == null) return null;
            return new LanguageDto
            {
                Code = language.Code,
                Id = language.Id,
                Name = language.Name
            };
        }

        public Language ConvertDtoToDB(LanguageDto languageDto)
        {
            if (languageDto == null) return null;
            return new Language
            {
                Code = languageDto.Code,
                Id = languageDto.Id,
                Name = languageDto.Name
            };
        }
    }
}