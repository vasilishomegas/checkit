using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class TemplateSortedProductDto : IDto
    {
        //FOR TemplateListOrdering:
        public int Id { get; set; }
        public int ShopId { get; set; }

        public string TemplateName { get; set; }

        //FOR TemplateSortedProduct
        public int CategoryId { get; set; }
        public int Rank { get; set; }

        IList<TemplateSortedProductDto> CategorySortings { get; set; }        
    }
}