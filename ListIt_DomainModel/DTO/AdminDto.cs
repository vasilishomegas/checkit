using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class AdminDto
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}