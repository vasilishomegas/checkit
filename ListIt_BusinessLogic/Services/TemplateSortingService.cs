using System;
using System.Collections.Generic;
using System.Linq;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class TemplateSortingService : Service<TemplateSortedProduct, TemplateSortedProductDto>
    {
        private readonly TemplateSortingRepository _templateRepository;
        public TemplateSortingService() : base(new TemplateSortingRepository())
        {
            _templateRepository = (TemplateSortingRepository)_repository;
        }

        public IList<TemplateSortedProductDto> GetTemplates()
        {
            List<TemplateSortedProductDto> templates = new List<TemplateSortedProductDto>();
            var all = _templateRepository.GetAll();
            if (all == null) return null;

            foreach(TemplateListOrdering listOrdering in all)
            {
                ChainRepository chainRepository = new ChainRepository();
                var names = chainRepository.GetShopAndChainNames((int)listOrdering.Shop_Id);
                string templateName = "Template";
                foreach(String name in names)
                {
                    templateName = templateName + " " + name;
                }

                templates.Add(ConvertDBToDto(listOrdering, templateName));
            }

            return templates;
        }

        public IList<ProductDto> SortByTemplate(int id, IList<ProductDto> products)
        {
            //var listOrdering = _templateRepository.GetListOrdering(id);
            var templates = _templateRepository.GetTemplates(id);
            List<ProductDto> sortedList = new List<ProductDto>();
            
            for (int x = 1; x <= templates.Count(); x++)
            {
                foreach (TemplateSortedProduct template in templates)
                {
                    //find template with according rank, starting by rank 1
                    if (template.Rank == x)
                    {
                        foreach (ProductDto product in products)
                        {
                            if (product.Category_Id == template.CategoryId)
                            {
                                sortedList.Add(product);
                            }
                        }
                    }            
                    
                }
            }

            return sortedList;

        }

        protected override TemplateSortedProductDto ConvertDBToDto(TemplateSortedProduct entity)
        {
            return StaticDBToDto(entity);
        }

        protected TemplateSortedProductDto ConvertDBToDto(TemplateListOrdering entity, string name)
        {
            return StaticDBToDto(entity, name);
        }

        protected TemplateSortedProductDto ConvertDBToDto(TemplateListOrdering ordering, TemplateSortedProduct template)
        {
            return StaticDBToDto(ordering, template);
        }

        protected override TemplateSortedProduct ConvertDtoToDB(TemplateSortedProductDto dto)
        {
            return StaticDtoToDB(dto);
        }

        protected TemplateListOrdering ConvertDtoToListOrderingDB(TemplateSortedProductDto dto)
        {
            return StaticDtoToListOrderingDB(dto);
        }

        public static TemplateListOrdering StaticDtoToListOrderingDB(TemplateSortedProductDto dto)
        {
            if (dto == null) return null;
            return new TemplateListOrdering
            {
                Id = dto.Id,
                Shop_Id = dto.ShopId
            };
        }

        public static TemplateSortedProduct StaticDtoToDB(TemplateSortedProductDto dto)
        {
            if (dto == null) return null;
            return new TemplateSortedProduct
            {
                TemplateListOrderingId = dto.Id,
                CategoryId = dto.CategoryId,
                Rank = dto.Rank,
                Timestamp = DateTime.Now
            };
        }

        public static TemplateSortedProductDto StaticDBToDto(TemplateListOrdering ordering, TemplateSortedProduct template)
        {
            if (ordering == null || template == null) return null;
            return new TemplateSortedProductDto
            {
                Id = ordering.Id,
                ShopId = ordering.Id,
                CategoryId = template.CategoryId,
                Rank = template.Rank
            };
        }

        public static TemplateSortedProductDto StaticDBToDto(TemplateListOrdering entity, string name)
        {
            if (entity == null) return null;
            return new TemplateSortedProductDto
            {
                Id = entity.Id,
                ShopId = (int)entity.Shop_Id,
                TemplateName = name
            };
        }

        public static TemplateSortedProductDto StaticDBToDto(TemplateSortedProduct entity)
        {
            if (entity == null) return null;
            return new TemplateSortedProductDto
            {
                Id = entity.TemplateListOrderingId,
                CategoryId = entity.CategoryId,
                Rank = entity.Rank
            };
        }
    }
}
