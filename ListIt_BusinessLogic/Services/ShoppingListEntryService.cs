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
    public class ShoppingListEntryService : Service<ShoppingListEntry, ShoppingListEntryDto>
    {
        private readonly ShoppingListEntryRepository _entryRepository;

        public ShoppingListEntryService() : base(new ShoppingListEntryRepository())
        {
            _entryRepository = (ShoppingListEntryRepository)_repository;
        }

        public int GetEntryId(int productId)
        {
            return _entryRepository.GetByProductId(productId).Id;
        }

        public new int Create(ShoppingListEntryDto dto)
        {
            var product = new Product
            {
                Id = dto.Product_Id,
                Timestamp = DateTime.Now,
                ProductType_Id = dto.ProductTypeId
            };

            ProductRepository productRepository = new ProductRepository();
            var prodId  = productRepository.CreateProduct(product);

            var entry = new ShoppingListEntry
            {
                Id = dto.Id,
                Quantity = dto.Quantity,
                Product_Id = prodId,
                ShoppingList_Id = dto.ShoppingList_Id,
                State_Id = dto.State_Id
            };

            _entryRepository.Create(entry);


            //sorting.Update() !! 

            return prodId;
        }
               

        public void Create(UserProductDto userProduct)
        {
            ProductRepository productRepository = new ProductRepository();
            productRepository.Create(new UserProduct
            {
                Id = userProduct.Id,
                Product_Id = userProduct.ProductId,
                Category_Id = userProduct.Category_Id,
                Currency_Id = userProduct.Currency_Id,
                UnitType_Id = userProduct.Unit_Id,
                User_Id = userProduct.User_Id,
                Name = userProduct.Name,
                Price = userProduct.Price
            });
        }       


        protected override ShoppingListEntryDto ConvertDBToDto(ShoppingListEntry entity)
        {
            return StaticDBToDto(entity);
        }

        protected override ShoppingListEntry ConvertDtoToDB(ShoppingListEntryDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static ShoppingListEntry StaticDtoToDB(ShoppingListEntryDto entryDto)
        {
            return new ShoppingListEntry
            {
                Id = entryDto.Id,
                Quantity = entryDto.Quantity,
                Product_Id = entryDto.Product_Id,
                ShoppingList_Id = entryDto.ShoppingList_Id,
                State_Id = entryDto.State_Id
            };
        }

        public static ShoppingListEntryDto StaticDBToDto(ShoppingListEntry entry)
        {
            // EACH Entry IS LINKED TO A 
            // PRODUCT: ProductType 
            // -> APiProducts/UserProducts/DefaultProducts: GET DETAILS HERE 
            // -> NameTranslationTable -> GET NAME HERE

            return new ShoppingListEntryDto
            {
                Id = entry.Id,
                Quantity = entry.Quantity,
                Product_Id = entry.Product_Id,
                ShoppingList_Id = entry.ShoppingList_Id,
                State_Id = entry.State_Id
            };
        }
    }
}