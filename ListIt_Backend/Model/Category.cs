using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class Category
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
    }
}