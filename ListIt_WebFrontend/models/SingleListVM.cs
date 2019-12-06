using System.Web;
using ListIt_DomainModel.DTO;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace ListIt_WebFrontend.Models
{
    public class SingleListVM : ShoppingListEntryDto
    {
        //public IList<ShoppingListEntryDto> Entries { get; set; }
        public IList<ProductDto> ListEntries { get; set; } 

        //FROM either UserProduct or APIProduct or DefaultProduct
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Range(0, 1000, ErrorMessage = "Please enter valid float Number")]
        public double Price { get; set; }
        public int UnitTypeId { get; set; }
        public int CategoryId { get; set; }
        public string UserCategory { get; set; }

        [Range(0, 10000, ErrorMessage = "Please enter valid float Number")]
        public int Quantity { get; set; }

        //List values:
        public int ListId { get; set; }
        //public int ListId { get; set; }
        public string ListName { get; set; }
        public int ListAccessTypeId { get; set; }

        //Lists for the forms:
        public IList<SelectListItem> UnitTypesList { get; set; }
        public int UnitTypesListId { get; set; }
        public IList<SelectListItem> CategoryList { get; set; }
        public int CategoryListId { get; set; }
        public IList<SelectListItem> CurrencyList { get; set; }
        public int CurrencyListId { get; set; }
        public IList<SelectListItem> ChooseProductsList { get; set; }
        public int ChosenProductId { get; set; }
    }
        

}
