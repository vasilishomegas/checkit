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
    
    public partial class TranslationOfUnitType
    {
        public int Language_Id { get; set; }
        public int UnitType_Id { get; set; }
        public string Translation { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual UnitType UnitType { get; set; }
    }
}
