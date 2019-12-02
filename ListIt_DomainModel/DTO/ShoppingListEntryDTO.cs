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


        //FORM Product Table
        public int ProductTypeId { get; set; }






        //public UserProductDto UserProduct
        //public APIProductDto APiProduct
        //public DefaultProductDto DefaultProduct


    }
}