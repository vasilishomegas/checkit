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
    public class LanguageService : Service<Language, LanguageDto>
    {
        private readonly LanguageRepository _languageRepository;
        private readonly LanguageConverter _languageConverter;

        public LanguageService() : base(new LanguageRepository(), new LanguageConverter())
        {
            _languageRepository = (LanguageRepository) _repository;
            _languageConverter = (LanguageConverter) _converter;
        }

        public LanguageDto GetByCode(string code)
        {
            var language = _languageRepository.GetByCode(code);
            return _languageConverter.ConvertDBToDto(language);
        }
    }
}
