﻿using System;
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
    public class CategoryService : Service<Category, CategoryDto>
    {
        private readonly CategoryRepository _catRepository;
        public CategoryService() : base(new CategoryRepository(), new CategoryConverter())
        {
            _catRepository = (CategoryRepository)_repository;
        }

        //Returns a list of default categories and user categories
        public IEnumerable<CategoryDto> GetCategories(int langId, int userId)
        {        
            List<CategoryDto> categoryList = new List<CategoryDto>();

            var defaultCatList = _catRepository.GetDefaultCategoryIds();
            foreach (Category cat in defaultCatList)
            {
                var translationInstance = _catRepository.Get(cat.Id, langId);
                //categoryList.Add(ConvertDBToDto(translationInstance));
                categoryList.Add(ConvertDBToDto(translationInstance));
            }

            var userCategories = _catRepository.GetUserCategoryIds(userId);
            foreach(Category cat in userCategories)
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
    }
}
