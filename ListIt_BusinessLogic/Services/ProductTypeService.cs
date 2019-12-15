using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class ProductTypeService : Service<ProductType, ProductTypeDto>
    {
        private readonly ProductTypeRepository _productTypeRepository;

        public ProductTypeService() : base(new ProductTypeRepository())
        {
            _productTypeRepository = (ProductTypeRepository)_repository;
        }
        protected override ProductTypeDto ConvertDBToDto(ProductType entity)
        {
            return StaticDBToDto(entity);
        }

        protected override ProductType ConvertDtoToDB(ProductTypeDto dto)
        {
            return StaticDtoToDB(dto);
        }

        private ProductTypeDto StaticDBToDto(ProductType entity)
        {
            return new ProductTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        private ProductType StaticDtoToDB(ProductTypeDto dto)
        {
            return new ProductType
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
