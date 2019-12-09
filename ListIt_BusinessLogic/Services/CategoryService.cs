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
    public class CategoryService : Service<Category, CategoryDto>
    {
        private readonly CategoryRepository _catRepository;
        public CategoryService() : base(new CategoryRepository())
        {
            _catRepository = (CategoryRepository)_repository;
        }

        public new int Create(CategoryDto dto)
        {
            dto.Id = _catRepository.Create(ConvertDtoToDB(dto));
            _catRepository.Create(ConvertDtoToTranslationDB(dto));

            return dto.Id;
        }

        public IEnumerable<CategoryDto> GetDefaultCategories(int langId)
        {
            List<CategoryDto> categoryList = new List<CategoryDto>();
            var defaultCatList = _catRepository.GetDefaultCategoryIds();
            foreach (Category cat in defaultCatList)
            {
                var translationInstance = _catRepository.Get(cat.Id, langId);
                categoryList.Add(ConvertDBToDto(translationInstance));
            }
            return categoryList;
        }

        //Returns a list of default categories and user categories
        public IEnumerable<CategoryDto> GetAllCategories(int langId, int userId)
        {        
            List<CategoryDto> categoryList = new List<CategoryDto>(GetDefaultCategories(langId));
            List<CategoryDto> userCategories = (List<CategoryDto>)GetUserCategories(langId, userId);
            foreach(CategoryDto categoryDto in userCategories)
            {
                categoryList.Add(categoryDto);
            }           

            return categoryList;
        }

        public IEnumerable<CategoryDto> GetUserCategories(int langId, int userId)
        {
            List<CategoryDto> categoryList = new List<CategoryDto>();
            var userCategories = _catRepository.GetUserCategoryIds(userId);
            foreach (Category cat in userCategories)
            {
                var translationInstance = _catRepository.Get(cat.Id, langId);
                categoryList.Add(ConvertDBToDto(translationInstance));
            }
            return categoryList;
        }

        protected CategoryDto ConvertDBToDto(TranslationOfCategory translation)
        {
            return StaticDBtranslationToDto(translation);
        }

        protected TranslationOfCategory ConvertDtoToTranslationDB(CategoryDto dto)
        {
            return StaticDtoToTranslationDB(dto);
        }

        protected override CategoryDto ConvertDBToDto(Category entity)
        {
            return StaticDBToDto(entity);
        }

        protected override Category ConvertDtoToDB(CategoryDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static Category StaticDtoToDB(CategoryDto dto)
        {
            if (dto == null) return null;
            return new Category
            {
                Id = dto.Id,
                User_Id = dto.UserId
            };
        }

        public static CategoryDto StaticDBToDto(Category cat)
        {
            if (cat == null) return null;
            return new CategoryDto
            {
                Id = cat.Id,
                UserId = cat.User_Id
            };
        }

        public static CategoryDto StaticDBtranslationToDto(TranslationOfCategory translation)
        {
            if (translation == null) return null;
            return new CategoryDto
            {
                Id = translation.Category_Id,
                Name = translation.Translation,
                LanguageId = translation.Language_Id
            };
        }

        public static TranslationOfCategory StaticDtoToTranslationDB(CategoryDto dto)
        {
            return new TranslationOfCategory
            {
                Category_Id = dto.Id,
                Language_Id = dto.LanguageId,
                Translation = dto.Name
            };
        }
    }
}
