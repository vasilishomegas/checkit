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
    public class ShoppingListConverter : IDtoDbConverter<ShoppingList, ShoppingListDto>, IShoppingListConverter
    {
        public ShoppingListDto ConvertDBToDto(ShoppingList list)
        {
            // EACH ShoppingListDto WILL CONTAIN VALUES FROM LinkUserToList AS WELL
            /*
            foreach (LinkUserToList link in list.LinkUserToLists)
            {

            }
            */

            if (list == null) return null;

            return new ShoppingListDto
            {
                Id = list.Id,
                Name = list.Name,
                Path = list.Path,
                //ChosenSortingId = null, //list.ChosenSorting_Id, NULLABLE in ShoppingList
                //ListAccessTypeId = list.LinkUserToLists
            };
        }

        public ShoppingList ConvertDtoToDB(ShoppingListDto listDto)
        {
            //  AS SOON AS THERE IS A ShoppingList CREATED THERE NEEDS ALSO TO BE 
            //  A LinkUserToList ENTRY CREATED

            // StaticListDtoToLinkDB(listDto); //Adding entry in LinkUserToList still needs to be added in Repository

            if (listDto == null) return null;

            return new ShoppingList
            {
                Id = listDto.Id,
                Name = listDto.Name,
                Path = "whatever???",
                Timestamp = DateTime.Now,
                ChosenSorting_Id = null,
            };
        }
    }
}
