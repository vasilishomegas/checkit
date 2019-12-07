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
using ListIt_BusinessLogic.Services.Converters;
using ListIt_DomainInterface.Interfaces.Repository;
using ListIt_DomainInterface.Interfaces.Converter;

namespace ListIt_BusinessLogic.Services
{
    public class ShoppingListEntryService : Service<ShoppingListEntry, ShoppingListEntryDto>
    {
        private readonly IShoppingListEntryRepository _shoppingListEntryRepository;
        private readonly IShoppingListEntryConverter _shoppingListEntryConverter;

        public ShoppingListEntryService() : this(new ShoppingListEntryConverter(), new ShoppingListEntryRepository())
        {

        }
        public ShoppingListEntryService(IShoppingListEntryConverter shoppingListEntryConverter, IShoppingListEntryRepository shoppingListEntryRepository) 
            : base(shoppingListEntryRepository, shoppingListEntryConverter)
        {
            _shoppingListEntryRepository = shoppingListEntryRepository;
            _shoppingListEntryConverter = shoppingListEntryConverter;
        }

        //If a new Entry is created, the update function of the SortingService must be called as well

        /*
        public new int Create(ShoppingListEntryDto dto)
        {
            var product = new Product
            {
                Id = dto.Product_Id,
                Timestamp = DateTime.Now,
                ProductType_Id = dto.ProductTypeId
            };

            var prodId  = _shoppingListEntryRepository.CreateProduct(product);
            ///= _entryRepository.GetIdOfProduct(product);

            var entry = new ShoppingListEntry
            {
                Id = dto.Id,
                Quantity = dto.Quantity,
                Product_Id = prodId,
                ShoppingList_Id = dto.ShoppingList_Id,
                State_Id = dto.State_Id
            };

            _shoppingListEntryRepository.Create(entry);


            //sorting.Update() !! 

            return prodId;
        }
        */

        public void Create(UserProductDto userProduct)
        {
            _shoppingListEntryRepository.CreateUserProduct(new UserProduct
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
            ShoppingListEntryRepository repo = new ShoppingListEntryRepository();
            var entryList = repo.GetEntriesByListId(listId);
            List<ShoppingListEntryDto> entryDtoList = new List<ShoppingListEntryDto>();


            foreach (ShoppingListEntry entry in entryList)
            {
                entryDtoList.Add(_shoppingListEntryConverter.ConvertDBToDto(entry));
            }

            return entryDtoList;
        }
    }
}