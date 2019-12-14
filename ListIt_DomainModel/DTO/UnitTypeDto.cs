using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class UnitTypeDto : IDto
    {
        public int Id { get; set; }

        /*
        //from translation table:
        public string Name { get; set; }
        public int LanguageId { get; set; }
        */
    }
}