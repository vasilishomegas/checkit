using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;


namespace ListIt_DomainModel.DTO
{
    public class ShopDto : IDto
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public virtual ChainDto Chain { get; set; }
    }
}