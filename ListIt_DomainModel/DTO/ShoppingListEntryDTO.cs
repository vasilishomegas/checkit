using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class ShoppingListEntryDto : IDto
    {
        public int Id { get; set; }
        public Nullable<int> Quantity { get; set; }
        public int Product_Id { get; set; }
        public int ShoppingList_Id { get; set; }
        public int State_Id { get; set; }

        //Adding (State and) Product here?? OR linking and retrieving values only in ViewModel??
        public ProductDto Product { get; set; }
        //public UserProductDto UserProduct
        //public APIProductDto APiProduct
        //public DefaultProductDto DefaultProduct


    }
}