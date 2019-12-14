using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class ShoppingListEntryConverter : IShoppingListEntryConverter
    {
        private readonly IShoppingListConverter _shoppingListConverter;
        private readonly IProductConverter _productConverter;
        private readonly IProductTypeConverter _productTypeConverter;

        public ShoppingListEntryConverter() : this(new ShoppingListConverter(), new ProductConverter(), new ProductTypeConverter())
        {

        }

        public ShoppingListEntryConverter(IShoppingListConverter shoppingListConverter, IProductConverter productConverter, IProductTypeConverter productTypeConverter)
        {
            _shoppingListConverter = shoppingListConverter;
            _productConverter = productConverter;
            _productTypeConverter = productTypeConverter;
        }

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
                Product = _productConverter.ConvertDBToDto(entry.Product),
                ShoppingList = _shoppingListConverter.ConvertDBToDto(entry.ShoppingList),
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
                Product_Id = entryDto.Product.Id,
                ShoppingList_Id = entryDto.ShoppingList.Id,
                State_Id = entryDto.State_Id,
                Product = _productConverter.ConvertDtoToDB(entryDto.Product),
                ShoppingList = _shoppingListConverter.ConvertDtoToDB(entryDto.ShoppingList)

                // TODO EntryState?
            };
        }
    }
}
