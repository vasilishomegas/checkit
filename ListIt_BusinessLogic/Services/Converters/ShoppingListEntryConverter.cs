using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainModel.DTO;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class ShoppingListEntryConverter : IDtoDbConverter<ShoppingListEntry, ShoppingListEntryDto>
    {
        public ShoppingListEntryDto ConvertDBToDto(ShoppingListEntry entry)
        {
            // EACH Entry IS LINKED TO A 
            // PRODUCT: ProductType 
            // -> APiProducts/UserProducts/DefaultProducts: GET DETAILS HERE 
            // -> NameTranslationTable -> GET NAME HERE

            if (entry == null) return null;

            return new ShoppingListEntryDto
            {
                Id = entry.Id,
                Quantity = entry.Quantity,
                Product_Id = entry.Product_Id,
                ShoppingList_Id = entry.ShoppingList_Id,
                State_Id = entry.State_Id
            };
        }

        public ShoppingListEntry ConvertDtoToDB(ShoppingListEntryDto entryDto)
        {
            // FOR EACH Entry THAT IS CREATED, A UserProduct NEEDS TO BE CREATED AS WELL
            //StaticDtoToProductDB(entryDto);

            if (entryDto == null) return null;

            return new ShoppingListEntry
            {
                Id = entryDto.Id,
                Quantity = entryDto.Quantity,
                Product_Id = entryDto.Product_Id,
                ShoppingList_Id = entryDto.ShoppingList_Id,
                State_Id = entryDto.State_Id
            };
        }
    }
}
