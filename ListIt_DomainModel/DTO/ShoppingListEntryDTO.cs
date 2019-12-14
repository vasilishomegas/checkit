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
        public int? Quantity { get; set; }
        public ProductDto Product { get; set; }
        public ShoppingListDto ShoppingList { get; set; }
        public int State_Id { get; set; }

        // TODO State

        //FROM Product Table

        // public ProductTypeDto ProductType { get; set; }   <==== Included in ProductDto

        




        //public UserProductDto UserProduct
        //public APIProductDto APiProduct
        //public DefaultProductDto DefaultProduct


    }
}