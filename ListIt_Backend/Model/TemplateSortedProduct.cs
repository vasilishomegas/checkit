using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class TemplateSortedProduct
    {
        public virtual TemplateListOrdering TemplateListOrdering { get; set; }
        public virtual Product Product { get; set; }
        public int Rank { get; set; }
        public DateTime Timestamp { get; set; }
    }
}