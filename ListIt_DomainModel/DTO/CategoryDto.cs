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
        public Nullable<int> UserId { get; set; }

        //from translation table:
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }
}