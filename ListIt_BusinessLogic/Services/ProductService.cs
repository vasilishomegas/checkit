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
using ListIt_DomainInterface.Interfaces.Service;

namespace ListIt_BusinessLogic.Services
{
    public class ProductService : IUserProductService, IApiProductService, IDefaultProductService, IProductService<Product, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductConverter _productConverter;


        public ProductService() : this(new ProductRepository(), new ProductConverter())
        {

        }

        public ProductService(IProductRepository productRepository, IProductConverter productConverter)
        {
            _productRepository = productRepository;
            _productConverter = productConverter;
        }

        public void Create(ProductDto dto)
        {
            if(dto is UserProductDto)
                _productRepository.Create(_productConverter.ConvertDtoToDB(dto), _productConverter.ConvertUserProductDtoToDb((UserProductDto) dto));

            else if(dto is ApiProductDto)
                _productRepository.Create(_productConverter.ConvertDtoToDB(dto), _productConverter.ConvertApiProductDtoToDb((ApiProductDto)dto));

            else if(dto is DefaultProductDto)
                _productRepository.Create(_productConverter.ConvertDtoToDB(dto), _productConverter.ConvertDefaultProductDtoToDb((DefaultProductDto)dto));
            
            else throw new Exception("Cannot insert an object of class ProductDto - use one of subclasses.");
        }

        public void Create(UserProductDto dto)
        {
            _productRepository.Create(_productConverter.ConvertDtoToDB(dto), _productConverter.ConvertUserProductDtoToDb(dto));
        }

        public void Create(ApiProductDto dto)
        {
            _productRepository.Create(_productConverter.ConvertDtoToDB(dto), _productConverter.ConvertApiProductDtoToDb(dto));
        }

        public void Create(DefaultProductDto dto)
        {
            _productRepository.Create(_productConverter.ConvertDtoToDB(dto), _productConverter.ConvertDefaultProductDtoToDb(dto));
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserProductDto Get(int id)
        {
            return _productConverter.ConvertUserProductToUserProductDto(_productRepository.GetUserProduct(id));
        }

        public IEnumerable<UserProductDto> GetAll()
        {
            return _productRepository.GetAllUserProducts().Select(userProduct => _productConverter.ConvertUserProductToUserProductDto(userProduct));
        }

        public void Update(UserProductDto dto)
        {
            _productRepository.Update(_productConverter.ConvertUserProductDtoToDb(dto));
        }

        public void Update(ApiProductDto dto)
        {
            throw new NotImplementedException();
        }

        public void Update(DefaultProductDto dto)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductDto dto)
        {
            throw new NotImplementedException();
        }

        ApiProductDto IService<ApiProduct, ApiProductDto>.Get(int id)
        {
            throw new NotImplementedException();
        }

        DefaultProductDto IService<DefaultProduct, DefaultProductDto>.Get(int id)
        {
            throw new NotImplementedException();
        }

        ProductDto IService<Product, ProductDto>.Get(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ApiProductDto> IService<ApiProduct, ApiProductDto>.GetAll()
        {
            throw new NotImplementedException();
        }

        IEnumerable<DefaultProductDto> IService<DefaultProduct, DefaultProductDto>.GetAll()
        {
            throw new NotImplementedException();
        }

        IEnumerable<ProductDto> IService<Product, ProductDto>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}