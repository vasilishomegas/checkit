using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class DefaultProduct : Product
    {
        public virtual Product Product { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual UnitType UnitType { get; set; }
        public int Price { get; set; }
    }
}