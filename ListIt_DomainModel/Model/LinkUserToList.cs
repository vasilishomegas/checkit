using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class LinkUserToList
    {
        public virtual User User { get; set; }
        public virtual ShoppingList ShoppingList{ get; set; }
        public virtual ListAccessType ListAccessType { get; set; }
    }
}