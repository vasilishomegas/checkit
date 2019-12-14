using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_BusinessLogic.Services.Interface;
using ListIt_DataAccess.Repository;
using ListIt_DataAccess.Repository.Interface;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class ProductTypeService : Service<ProductType, ProductTypeDto>, IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IProductTypeConverter _productTypeConverter;

        public ProductTypeService() : this(new ProductTypeRepository(), new ProductTypeConverter())
        {

        }

        public ProductTypeService(IProductTypeRepository productTypeRepository, IProductTypeConverter productTypeConverter) : base(productTypeRepository, productTypeConverter)
        {

        }
    }
}
