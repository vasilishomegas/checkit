using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class ShopApi
    {
        [ForeignKey("Chain")]
        public int Id { get; set; }
        public virtual Chain Chain { get; set; }
        public string Url { get; set; }
    }
}