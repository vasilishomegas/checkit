//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ListIt_DataAccessModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class LinkUserToList
    {
        public int UserId { get; set; }
        public int ShoppingListId { get; set; }
        public int ListAccessTypeId { get; set; }
    
        public virtual ListAccessType ListAccessType { get; set; }
        public virtual ShoppingList ShoppingList { get; set; }
    }
}
