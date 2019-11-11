using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class ApiProduct : Product
    {
        public virtual DefaultProduct DefaultProduct { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual UnitType Unit { get; set; }
        public virtual ShopApi ShopApi { get; set; }
        public string Endpoint { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}