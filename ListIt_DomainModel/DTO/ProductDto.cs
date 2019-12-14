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
        public ProductTypeDto ProductType { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class ApiProductDto : ProductDto
    {
        public string Name { get; set; }
        public CurrencyDto Currency { get; set; }
        public UnitTypeDto UnitType { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public DefaultProductDto DefaultProduct { get; set; }
        public ShopApiDto ShopApi { get; set; }        
        public string Endpoint { get; set; }
        
    }

    public class DefaultProductDto : ProductDto
    {
        public string Name { get; set; }
        public CurrencyDto Currency { get; set; }
        public UnitTypeDto UnitType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public CategoryDto Category { get; set; }
    }

    public class UserProductDto : ProductDto
    {
        public string Name { get; set; }
        public CurrencyDto Currency { get; set; }
        public UnitTypeDto UnitType { get; set; }
        // public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public CategoryDto Category { get; set; }
        public UserDto User { get; set; }
    }
}