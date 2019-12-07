using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_DomainModel.DTO
{
    public class UserProductDto : ProductDto
    {
        public int? CategoryId { get; set; }
        public int UserId { get; set; }
    }
}