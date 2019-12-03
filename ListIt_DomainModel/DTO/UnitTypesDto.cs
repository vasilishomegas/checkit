using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class UnitTypesDto : IDto
    {
        public int Id { get; set; }

        //from translatin table:
        public string Name { get; set; }
        public int LanguageId { get; set; }
    }
}