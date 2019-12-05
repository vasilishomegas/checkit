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

        //Getting all shoppinglistentries and converting to ProductDto
        public IList<ProductDto> GetEntriesAsProducts(int listId)
        {
            //need to get Product, ShoppingListEntry and UserProduct for each entry
            List<ProductDto> productDtoList = new List<ProductDto>();

            //1. get ShoppingListEntries
            ShoppingListEntryService entryService = new ShoppingListEntryService();
            var entriesList = entryService.GetEntriesByListId(listId);

            foreach(ShoppingListEntryDto entry in entriesList)
            {
                //2. get Products
                var product = _prodRepository.Get(entry.Product_Id);

                //3. get UserProduct
                if(product.ProductType_Id == 3 || product.ProductType_Id == 4)
                {
                    var userProduct = _prodRepository.GetUserProduct(entry.Product_Id);
                }

                //4. Create ProductDto and add to list
                var productDto = ConvertDBToDto(product, userProduct:, entry);
                productDtoList.Add(productDto);
            }

            return productDtoList;
        }



        protected ProductDto ConvertDBToDto(Product entity, UserProduct userProduct, ShoppingListEntry entry)
        {
            return StaticDBToDto(entity, userProduct, entry);
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
                Currency_Id = (int)userProduct.Currency_Id,
                Unit_Id = (int)userProduct.UnitType_Id,
                //Quantity = from ProductTable
                Price = (int)userProduct.Price,
                Category_Id = (int)userProduct.Category_Id,
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

        public static ProductDto StaticDBToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                ProductTypeId = product.ProductType_Id
            };
        }

        public static ProductDto StaticDBToDto(Product product, UserProduct userProduct, ShoppingListEntry entry)
        {
            return new ProductDto
            {
                Id = product.Id,
                ProductTypeId = product.ProductType_Id,
                Name = userProduct.Name,
                Currency_Id = (int)userProduct.Currency_Id,
                Unit_Id = (int)userProduct.UnitType_Id,
                Quantity = (int)entry.Quantity,
                Price = (decimal)userProduct.Price,
                ProductId = product.Id,
                Category_Id = userProduct.Category_Id
            };
        }
    }
}
