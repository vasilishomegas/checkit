using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class CategoryDto : IDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }

        //from translation table:
        // public LanguageDto Language { get; set; }
        // public string Name { get; set; }
    }
}