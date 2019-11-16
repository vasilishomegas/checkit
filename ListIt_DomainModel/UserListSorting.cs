//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ListIt_DomainModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserListSorting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserListSorting()
        {
            this.ShoppingLists = new HashSet<ShoppingList>();
            this.UserEntrySortings = new HashSet<UserEntrySorting>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Shop_Id { get; set; }
        public int ShoppingList_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
        public virtual ShoppingList ShoppingList { get; set; }
        public virtual Shop Shop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserEntrySorting> UserEntrySortings { get; set; }
    }
}
