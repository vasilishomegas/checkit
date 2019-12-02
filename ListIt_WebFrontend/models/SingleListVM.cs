using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO;
using System.ComponentModel.DataAnnotations;

namespace ListIt_WebFrontend.Models
{
    public class SingleListVM : ShoppingListEntryDto
    {
        //Holding a list of all Entries of a single List -> displayed in detailed view
    
        public IList<ShoppingListEntryDto> Entries { get; set; }

        //FROM either UserProduct or APIProduct or DefaultProduct
        public string Name { get; set; }
        public double Price { get; set; }
        public int UnitTypeId { get; set; }
        public int CategoryId { get; set; }
        public string UserCategory { get; set; }

        
        //List values:
        public int ListId { get; set; }
        public string ListName { get; set; }
        public int ListAccessTypeId { get; set; }

    }
}