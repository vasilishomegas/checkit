using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public virtual ProductType ProductType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}