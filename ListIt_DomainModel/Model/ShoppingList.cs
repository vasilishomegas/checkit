using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public virtual UserListOrdering UserListOrdering { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime Timestamp { get; set; }
    }
}