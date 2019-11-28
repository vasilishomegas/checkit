using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using System.Security.Cryptography;

namespace ListIt_BusinessLogic.Services
{
    public class ProductService : Service<Product, ProductDto>
    {
        private readonly ProductRepository _prodRepository;

        public ProductService() : base(new ProductRepository())
        {
            _prodRepository = (ProductRepository)_repository;
        }

        public void Create(UserProductDto dto)
        {
            _prodRepository.Create(StaticDtoToDB(dto));
        }

        protected override ProductDto ConvertDBToDto(Product entity)
        {
            return StaticDBToDto(entity);
        }

        protected override Product ConvertDtoToDB(ProductDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static UserProduct StaticDtoToDB(UserProductDto dto)
        {
            return new UserProduct
            {
                Id = dto.ProductTypeId,
                Category_Id = dto.Category_Id,
                Currency_Id = dto.Currency_Id,
                UnitType_Id = dto.Unit_Id,
                User_Id = dto.User_Id,
                Name = dto.Name,
                Price = dto.Price
            };
        }

        public static UserProductDto StaticDBToDto(UserProduct userProduct)
        {    

            return new UserProductDto
            {
                Id = userProduct.Id,
                Name = userProduct.Name,
                Currency_Id = userProduct.Currency_Id,
                Unit_Id = userProduct.UnitType_Id,
                //Quantity = from ProductTable
                Price = userProduct.Price,
                Category_Id = userProduct.Category_Id,
                User_Id = userProduct.User_Id,
                //ProductTypeId = from ProductTable
            };
        }

        public static Product StaticDtoToDB(ProductDto dto)
        {
            return new Product
            {
                Id = dto.ProductTypeId,
                Timestamp = DateTime.Now,
                ProductType_Id = dto.ProductTypeId,
            };
        }

        public static ProductDto StaticDBToDto(Product entry)
        {
            return new ProductDto
            {
                Id = entry.Id,
                ProductTypeId = entry.ProductType_Id
            };
        }
    }
}
