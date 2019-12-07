using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using System.Security.Cryptography;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_DomainInterface.Interfaces.Repository;
using ListIt_DomainInterface.Interfaces.Converter;

namespace ListIt_BusinessLogic.Services
{
    public class ProductService : Service<Product, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductConverter _productConverter;


        public ProductService() : this(new ProductRepository(), new ProductConverter())
        {

        }

        public ProductService(IProductRepository productRepository, IProductConverter productConverter) : base(productRepository, productConverter)
        {
            _productRepository = productRepository;
            _productConverter = productConverter;
        }

        /*
        public void Create(UserProductDto dto)
        {
            _productRepository.Create(_productConverter.ConvertDtoToDB(dto));
        }
        */
    }
}
