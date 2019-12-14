using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters.Interface
{
    public interface IShoppingListConverter : IDtoDbConverter<ShoppingList, ShoppingListDto>
    {
        ShoppingListDto ConvertDBToDto(ShoppingList shoppingList);
        ShoppingList ConvertDtoToDB(ShoppingListDto shoppingListDto);
    }
}
