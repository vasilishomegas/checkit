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
    
    public partial class ListAccessType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ListAccessType()
        {
            this.LinkUserToLists = new HashSet<LinkUserToList>();
        }
    
        public int Id { get; set; }
        public string Relation { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LinkUserToList> LinkUserToLists { get; set; }
    }
}
