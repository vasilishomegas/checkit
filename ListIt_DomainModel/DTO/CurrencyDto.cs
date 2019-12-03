using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class CurrencyDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sign { get; set; }
        public string Code { get; set; }
    }
}