using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccess.Repository.Interface;
using ListIt_DataAccessModel;
using ListIt_DomainModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class LanguageService : Service<Language, LanguageDto>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ILanguageConverter _languageConverter;

        public LanguageService(): this(new LanguageRepository(), new LanguageConverter())
        {
            
        }

        public LanguageService(ILanguageRepository languageRepository, ILanguageConverter languageConverter) : base(languageRepository, languageConverter)
        {
            _languageRepository = languageRepository;
            _languageConverter = languageConverter;
        }

        public LanguageDto GetByCode(string code)
        {
            var language = _languageRepository.GetByCode(code);
            return _languageConverter.ConvertDBToDto(language);
        }
    }
}
