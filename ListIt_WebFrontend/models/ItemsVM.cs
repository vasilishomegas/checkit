using System.Web;
using ListIt_DomainModel.DTO;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;


namespace ListIt_WebFrontend.Models
{
    public class ItemsVM : ProductDto
    {
        //FOR Item edit view
        [Range(0, 1000, ErrorMessage = "Please enter valid float Number")]
        public uint Quantity { get; set; }

        public string UserCategory { get; set; }

        //List values:
        public int ListId { get; set; }

        //Lists for the forms:
        public IList<SelectListItem> UnitTypesList { get; set; }
        public int UnitTypesListId { get; set; }
        public IList<SelectListItem> CategoryList { get; set; }
        public int CategoryListId { get; set; }
        public IList<SelectListItem> CurrencyList { get; set; }
        public int CurrencyListId { get; set; }

    }
}