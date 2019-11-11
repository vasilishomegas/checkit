using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class TemplateListOrdering
    {
        public int Id { get; set; }
        public virtual Shop Shop { get; set; }
    }
}