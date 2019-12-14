using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class CategoryConverter : ICategoryConverter
    {
        private readonly IUserConverter _userConverter;
        // private readonly ILanguageConverter _languageConverter;

        public CategoryConverter() : this(new UserConverter())
        {

        }

        public CategoryConverter(IUserConverter userConverter)
        {
            _userConverter = userConverter;
        }

        public CategoryDto ConvertDBToDto(Category category)
        {
            if (category == null) return null;
            return new CategoryDto
            {
                Id = category.Id,
                User = _userConverter.ConvertDBToDto(category.User)
            };
        }

        public Category ConvertDtoToDB(CategoryDto dto)
        {
            if (dto == null) return null;
            return new Category
            {
                Id = dto.Id,
                User_Id = dto.User.Id,
                User = _userConverter.ConvertDtoToDB(dto.User)
            };
        }
    }
}
