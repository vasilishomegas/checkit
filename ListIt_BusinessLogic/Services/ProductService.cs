using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using System.Security.Cryptography;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Interface;
using ListIt_DataAccess.Repository.Interface;

namespace ListIt_BusinessLogic.Services
{
    public class ProductService : IUserProductService, IApiProductService, IDefaultProductService, IProductService<Product, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductConverter _productConverter;
        private readonly IProductTypeService _productTypeService;
        private readonly ICurrencyService _currencyService;
        private readonly IUnitTypeService _unitTypeService;

        public ProductService() : this(new ProductRepository(), new ProductConverter(), new ProductTypeService(), new CurrencyService(), new UnitTypeService())
        {

        }

        public ProductService(IProductRepository productRepository, IProductConverter productConverter, IProductTypeService productTypeService, ICurrencyService currencyService, IUnitTypeService unitTypeService)
        {
            _productRepository = productRepository;
            _productConverter = productConverter;
            _productTypeService = productTypeService;
            _currencyService = currencyService;
            _unitTypeService = unitTypeService;
        }

        public void Create(ProductDto dto)
        {
            dto.Timestamp = DateTime.Now;
            switch (dto)
            {
                case UserProductDto productDto:
                    productDto.ProductType.Id = 2;
                    _productRepository.Create(_productConverter.ConvertDtoToDB(productDto), _productConverter.ConvertUserProductDtoToDb(productDto));
                    break;
                case ApiProductDto productDto:
                    productDto.ProductType.Id = 1;
                    _productRepository.Create(_productConverter.ConvertDtoToDB(productDto), _productConverter.ConvertApiProductDtoToDb(productDto));
                    break;
                case DefaultProductDto productDto:
                    productDto.ProductType.Id = 3;
                    _productRepository.Create(_productConverter.ConvertDtoToDB(productDto), _productConverter.ConvertDefaultProductDtoToDb(productDto));
                    break;
                default:
                    throw new Exception("Cannot insert an object of class ProductDto - use one of subclasses.");
            }
        }

        public void Create(UserProductDto dto)
        {
            dto.Timestamp = DateTime.Now;
            dto.ProductType = _productTypeService.Get(2);
            _productRepository.Create(_productConverter.ConvertDtoToDB(dto), _productConverter.ConvertUserProductDtoToDb(dto));
        }

        public void Create(ApiProductDto dto)
        {
            dto.Timestamp = DateTime.Now;
            dto.ProductType = _productTypeService.Get(1);
            _productRepository.Create(_productConverter.ConvertDtoToDB(dto), _productConverter.ConvertApiProductDtoToDb(dto));
        }

        public void Create(DefaultProductDto dto)
        {
            // TODO Refactor to use just service
            dto.Timestamp = DateTime.Now;
            dto.ProductType = _productTypeService.Get(3);
            dto.UnitType = _unitTypeService.Get(dto.UnitType.Id);
            dto.Currency = _currencyService.Get(dto.Currency.Id);
            _productRepository.Create(_productConverter.ConvertDtoToDB(dto), _productConverter.ConvertDefaultProductDtoToDb(dto));
        }

        

        public IEnumerable<ProductDto> GetAll()
        {
            // TODO SEND SPECIAL QUERY TO DB
            var apiProducts = _productRepository.GetAllApiProducts().Select(subProduct =>
            {
                var product = _productConverter.ConvertApiProductDbToDto(subProduct);
                var superProduct = _productRepository.Get(subProduct.Id);
                product.Timestamp = superProduct.Timestamp;
                product.ProductType = _productTypeService.Get(superProduct.ProductType_Id);
                return product;
            }).ToList();
            
            
            var defaultProducts = _productRepository.GetAllDefaultProducts().Select(subProduct =>
            {
                var product = _productConverter.ConvertDefaultProductDbToDto(subProduct);
                var superProduct = _productRepository.Get(subProduct.Id);
                product.Timestamp = superProduct.Timestamp;
                product.ProductType = _productTypeService.Get(superProduct.ProductType_Id);
                return product;
            }).ToList(); 
            
            var userProducts = _productRepository.GetAllUserProducts().Select(subProduct =>
            {
                var product = _productConverter.ConvertUserProductToUserProductDto(subProduct);
                var superProduct = _productRepository.Get(subProduct.Id);
                product.Timestamp = superProduct.Timestamp;
                product.ProductType = _productTypeService.Get(superProduct.ProductType_Id);
                return product;
            }).ToList();

            var products = new List<ProductDto>();

            /*
            var superProducts = _productRepository.GetAll();
            foreach (var productDto in products)
            {
                foreach (var superProduct in superProducts)
                {
                    if (superProduct.Id == productDto.Id)
                    {
                        productDto.Timestamp = superProduct.Timestamp;
                    }
                }
            }*/

            products.AddRange(apiProducts);
            products.AddRange(defaultProducts);
            products.AddRange(userProducts);

            return products;
        }

        public ProductDto Get(int id)
        {
            var product = _productRepository.Get(id);
            if (product == null) throw new KeyNotFoundException("Cannot find a product with such ID.");
            var productType = _productTypeService.Get(product.ProductType_Id);

            if(productType == null) throw new Exception("ProductType was not found.");

            switch (productType.Name)
            {
                case "UserProduct":
                    return _productConverter.ConvertUserProductToUserProductDto(_productRepository.GetUserProduct(id));
                case "DefaultProduct":
                    return _productConverter.ConvertDefaultProductDbToDto(_productRepository.GetDefaultProduct(id));
                case "ApiProduct":
                    return _productConverter.ConvertApiProductDbToDto(_productRepository.GetApiProduct(id));
                default:
                    throw new KeyNotFoundException("Product type was found, but unknown: " + productType.Name);
            }
        }

        public void Update(UserProductDto dto)
        {
            _productRepository.Update(_productConverter.ConvertUserProductDtoToDb(dto));
            _productRepository.Update(_productConverter.ConvertDtoToDB(dto));
        }

        public void Update(ApiProductDto dto)
        {
            _productRepository.Update(_productConverter.ConvertApiProductDtoToDb(dto));
            _productRepository.Update(_productConverter.ConvertDtoToDB(dto));
        }

        public void Update(DefaultProductDto dto)
        {

            _productRepository.Update(_productConverter.ConvertDefaultProductDtoToDb(dto));
            _productRepository.Update(_productConverter.ConvertDtoToDB(dto));
        }

        public void Update(ProductDto dto)
        {
            throw new Exception("Choose one of subclasses of Product.");
        }

        public void Delete(int id)
        {
            var product = _productRepository.Get(id);
            if (product == null) throw new KeyNotFoundException("Cannot find a product with such ID");
            var productType = _productTypeService.Get(product.ProductType_Id);

            if (productType == null) throw new Exception("ProductType was not found.");

            switch (productType.Name)
            {
                case "UserProduct":
                    _productRepository.Delete(new UserProduct { Id = id });
                    break;
                case "DefaultProduct":
                    _productRepository.Delete(new DefaultProduct { Id = id });
                    break;
                case "ApiProduct":
                    _productRepository.Delete(new ApiProduct { Id = id });
                    break;
                default:
                    _productRepository.Delete(id);
                    break;
            }
        }

        IEnumerable<DefaultProductDto> IService<DefaultProduct, DefaultProductDto>.GetAll()
        {
            return _productRepository.GetAllDefaultProducts().Select(product => _productConverter.ConvertDefaultProductDbToDto(product));
        }

        DefaultProductDto IService<DefaultProduct, DefaultProductDto>.Get(int id)
        {
            return _productConverter.ConvertDefaultProductDbToDto(_productRepository.GetDefaultProduct(id));
        }

        IEnumerable<ApiProductDto> IService<ApiProduct, ApiProductDto>.GetAll()
        {
            return _productRepository.GetAllApiProducts().Select(product => _productConverter.ConvertApiProductDbToDto(product));
        }

        ApiProductDto IService<ApiProduct, ApiProductDto>.Get(int id)
        {
            return _productConverter.ConvertApiProductDbToDto(_productRepository.GetApiProduct(id));
        }

        IEnumerable<UserProductDto> IService<UserProduct, UserProductDto>.GetAll()
        {
            return _productRepository.GetAllUserProducts().Select(product => _productConverter.ConvertUserProductToUserProductDto(product));
        }

        UserProductDto IService<UserProduct, UserProductDto>.Get(int id)
        {
            return _productConverter.ConvertUserProductToUserProductDto(_productRepository.GetUserProduct(id));
        }
    }
}