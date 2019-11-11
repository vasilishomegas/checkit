using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class TranslationOfUnitType
    {
        public virtual UnitType UnitType { get; set; }
        public virtual Language Language { get; set; }
        public string Translation { get; set; }
    }
}