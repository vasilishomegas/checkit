using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO;
using System.ComponentModel.DataAnnotations;

namespace ListIt_WebFrontend.Models
{
    public class ListsVM : ShoppingListDto
    {
        //Holding a List of all UserLists -> showing in YourLists

        /* ATTRIBUTES FROM SHOPPINGLISTDTO */
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Path { get; set; }
        //public List<ShoppingListEntryDto> Entries { get; set; }
        //public Nullable<int> ChosenSortingId { get; set; }

        ////FROM LinkUserToList.cs in DataAccessModels:
        //public int ListAccessTypeId { get; set; }
        //public int UserId { get; set; }


        public IList<ShoppingListDto> AllUserLists { get; set; }
    }
}