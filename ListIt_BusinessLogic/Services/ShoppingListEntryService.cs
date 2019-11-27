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
    class ShoppingListEntryService : Service<ShoppingListEntry, ShoppingListEntryDto>
    {
        private readonly ShoppingListEntryRepository _entryRepository;

        public ShoppingListEntryService() : base(new ShoppingListEntryRepository())
        {
            _entryRepository = (ShoppingListEntryRepository)_repository;
        }

        //If a new Entry is created, the update function of the SortingService must be called as well
        // public override Create() ??


        public IList<ShoppingListEntryDto> GetEntriesByListId(int listId)
        {
            ShoppingListEntryRepository repo = new ShoppingListEntryRepository();
            var entryList = repo.GetEntriesByListId(listId);
            List<ShoppingListEntryDto> entryDtoList = new List<ShoppingListEntryDto>();


            foreach(ShoppingListEntry entry in entryList)
            {
                entryDtoList.Add(ConvertDomainToDto(entry));
            }

            return entryDtoList;
        }


        protected override ShoppingListEntryDto ConvertDomainToDto(ShoppingListEntry entity)
        {
            return StaticDomainToDto(entity);
        }

        protected override ShoppingListEntry ConvertDtoToDomain(ShoppingListEntryDto dto)
        {
            return StaticDtoToDomain(dto);
        }

        public static ShoppingListEntry StaticDtoToDomain(ShoppingListEntryDto entryDto)
        {
            // FOR EACH Entry THAT IS CREATED, A UserProduct NEEDS TO BE CREATED AS WELL
           
            return new ShoppingListEntry
            {
               Id = entryDto.Id,
               Quantity = entryDto.Quantity,
               Product_Id = entryDto.Product_Id,
               ShoppingList_Id = entryDto.ShoppingList_Id,
               State_Id = entryDto.State_Id
            };
        }

        public static ShoppingListEntryDto StaticDomainToDto(ShoppingListEntry entry)
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
                //Name = from APiProducts/UserProducts/DefaultProducts -> TranslationFile
                //Quantity = from APiProducts/UserProducts/DefaultProducts
                //Price: from APiProducts/UserProducts/DefaultProducts
            };
        }
    }
}
