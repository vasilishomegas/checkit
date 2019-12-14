using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class ProductTypeConverter : IProductTypeConverter
    {
        public ProductTypeDto ConvertDBToDto(ProductType entity)
        {
            if (entity == null) return null;
            return new ProductTypeDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public ProductType ConvertDtoToDB(ProductTypeDto dto)
        {
            if (dto == null) return null;
            return new ProductType
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
