using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public virtual Chain Chain { get; set; }
        public string Address { get; set; }
    }
}