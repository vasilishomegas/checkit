using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class LinkUserToDefaultProduct
    {
        public virtual User User { get; set; }
        public virtual DefaultProduct DefaultProduct { get; set; }
        public DateTime Timestamp { get; set; }

    }
}