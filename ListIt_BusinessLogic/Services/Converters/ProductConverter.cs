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
    public class ProductConverter : IDtoDbConverter<Product, ProductDto>, IProductConverter
    {
        public UserProduct ConvertApiProductDtoToDb(ApiProductDto dto)
        {
            if (dto == null) return null;
            return new UserProduct
            {
                Id = dto.Id,
                Name = dto.Name,
                Currency_Id = dto.Currency_Id,
                Product_Id = dto.ProductId,
                UnitType_Id = dto.Unit_Id,
                Price = dto.Price
            };
        }

        public ProductDto ConvertDBToDto(Product entry)
        {
            if (entry == null) return null;
            return new ProductDto
            {
                Id = entry.Id,
                ProductTypeId = entry.ProductType_Id
            };
        }

        public UserProduct ConvertDefaultProductDtoToDb(DefaultProductDto dto)
        {
            if (dto == null) return null;
            return new UserProduct
            {
                Id = dto.Id,
                Name = dto.Name,
                Currency_Id = dto.Currency_Id,
                Product_Id = dto.ProductId,
                UnitType_Id = dto.Unit_Id,
                Price = dto.Price
            };
        }

        public Product ConvertDtoToDB(ProductDto dto)
        {
            if (dto == null) return null;
            return new Product
            {
                Id = dto.ProductTypeId,
                Timestamp = DateTime.Now,
                ProductType_Id = dto.ProductTypeId,
            };
        }

        public UserProduct ConvertUserProductDtoToDb(UserProductDto dto)
        {
            if (dto == null) return null;
            return new UserProduct
            {
                Id = dto.Id,
                Category_Id = dto.CategoryId,
                Name = dto.Name,
                Currency_Id = dto.Currency_Id,
                Product_Id = dto.ProductId,
                UnitType_Id = dto.Unit_Id,
                Price = dto.Price,
                User_Id = dto.UserId
            };
        }

        public UserProductDto ConvertUserProductToUserProductDto(UserProduct product)
        {
            if (product == null) return null;
            return new UserProductDto
            {
                Id = product.Id,
                CategoryId = product.Category_Id,
                Price = product.Price,
                Currency_Id = product.Currency_Id,
                Name = product.Name,
                ProductId = product.Product_Id,
                Unit_Id = product.UnitType_Id,
                UserId = product.User_Id,
            };
        }
    }
}