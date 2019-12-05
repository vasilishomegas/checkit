using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class ProductConverter : IDtoDbConverter<Product, ProductDto>
    {
        public ProductDto ConvertDBToDto(Product entry)
        {
            return new ProductDto
            {
                Id = entry.Id,
                ProductTypeId = entry.ProductType_Id
            };
        }

        public Product ConvertDtoToDB(ProductDto dto)
        {
            return new Product
            {
                Id = dto.ProductTypeId,
                Timestamp = DateTime.Now,
                ProductType_Id = dto.ProductTypeId,
            };
        }
    }
}