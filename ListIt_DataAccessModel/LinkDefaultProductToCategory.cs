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
    
    public partial class LinkDefaultProductToCategory
    {
        public int DefaultProductId { get; set; }
        public int CategoryId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual DefaultProduct DefaultProduct { get; set; }
    }
}
