using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class LinkDefaultProductToCategory
    {
        public virtual DefaultProduct DefaultProduct { get; set; }
        public virtual Category Category { get; set; }
    }
}