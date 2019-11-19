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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.ShoppingListEntries = new HashSet<ShoppingListEntry>();
            this.TemplateSortedProducts = new HashSet<TemplateSortedProduct>();
            this.TranslationOfProducts = new HashSet<TranslationOfProduct>();
        }
    
        public int Id { get; set; }
        public System.DateTime Timestamp { get; set; }
        public int ProductType_Id { get; set; }
    
        public virtual ApiProduct ApiProduct { get; set; }
        public virtual DefaultProduct DefaultProduct { get; set; }
        public virtual ProductType ProductType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingListEntry> ShoppingListEntries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TemplateSortedProduct> TemplateSortedProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TranslationOfProduct> TranslationOfProducts { get; set; }
        public virtual UserProduct UserProduct { get; set; }
    }
}