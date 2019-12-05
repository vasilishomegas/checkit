using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class ProductDto : IDto
    {
        public int Id { get; set; } 
        public int ProductTypeId { get; set; }

        

        //Generic Values
        public string Name { get; set; }
        public int Currency_Id { get; set; }
        public int Unit_Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public Nullable<int> Category_Id { get; set; }
    }

    public class ApiProductDto : ProductDto
    {
        public Nullable<int> DefaultProduct_Id { get; set; }
        public int ShopApi_Id { get; set; }        
        public string Endpoint { get; set; }
        
    }

    public class UserProductDto : ProductDto
    {
        //public int Id { get; set; }        
        //public Nullable<int> Currency_Id { get; set; }
        //public new Nullable<int> Unit_Id { get; set; }
        //public new Nullable<decimal> Price { get; set; }
        public int User_Id { get; set; }

    }

    public class DefaultProductDto : ProductDto
    {
        //public int Category_Id { get; set; }

        //Own table for names and categories

    }
}