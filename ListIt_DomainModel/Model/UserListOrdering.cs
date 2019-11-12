using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class UserListOrdering
    {
        [ForeignKey("ShoppingList")]
        public int Id { get; set; }
        public virtual ShoppingList ShoppingList { get; set; }
        public virtual Shop Shop { get; set; }
        public string Name { get; set; }
    }
}