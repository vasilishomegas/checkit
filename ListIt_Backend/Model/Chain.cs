using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_Backend.Models
{
    public class Chain
    {
        public int Id { get; set; }
        public virtual ShopApi ShopApi { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
    }
}