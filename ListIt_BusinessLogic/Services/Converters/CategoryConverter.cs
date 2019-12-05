using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class CategoryConverter : IDtoDbConverter<Category, CategoryDto>
    {
        public CategoryDto ConvertDBToDto(Category category)
        {
            if (category == null) return null;
            return new CategoryDto
            {
                Id = category.Id,
                UserId = category.User_Id
            };
        }

        public Category ConvertDtoToDB(CategoryDto dto)
        {
            if (dto == null) return null;
            return new Category
            {
                Id = dto.Id,
                User_Id = dto.UserId
            };
        }
    }
}
