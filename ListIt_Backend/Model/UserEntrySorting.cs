using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class UserEntrySorting
    {
        public virtual UserListOrdering UserListOrdering { get; set; }
        public virtual ShoppingListEntry ShoppingListEntry { get; set; }
        public virtual ShoppingListEntry PrevEntryId { get; set; }
        public virtual ShoppingListEntry NextEntryId { get; set; }
    }
}