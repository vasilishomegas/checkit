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

        public int GetEntryId(int productId, int listId)
        {
            return _entryRepository.GetByProductAndListId(productId, listId).Id;
        }

        public override void Create(ShoppingListEntryDto dto)
        {
            _entryRepository.Create(new ShoppingListEntry
            {
                Id = dto.Id,
                Quantity = dto.Quantity,
                Product_Id = dto.Product_Id,
                ShoppingList_Id = dto.ShoppingList_Id,
                State_Id = dto.State_Id
            });

            //sorting.Update() !! 
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
        
        public IList<ShoppingListEntryDto> GetEntriesByListId(int listId)
        {
            var list = _entryRepository.GetEntriesByListId(listId);
            List<ShoppingListEntryDto> entries = new List<ShoppingListEntryDto>();
            foreach(ShoppingListEntry entry in list)
            {
                entries.Add(ConvertDBToDto(entry));
            }
            return entries;
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