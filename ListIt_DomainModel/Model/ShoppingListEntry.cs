using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class ShoppingListEntry
    {
        public int Id { get; set; }
        public virtual ShoppingList ShoppingList { get; set; }
        public virtual EntryState State { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}