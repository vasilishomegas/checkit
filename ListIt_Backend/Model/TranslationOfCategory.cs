using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class TranslationOfCategory
    {
        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
        public string Translation { get; set; }
    }
}