using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class ProductConverter : IDtoDbConverter<Product, ProductDto>, IProductConverter
    {
        private readonly IProductTypeConverter _productTypeConverter;
        private readonly ICategoryConverter _categoryConverter;
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IUnitTypeConverter _unitTypeConverter;
        private readonly IUserConverter _userConverter;
        private readonly IShopApiConverter _shopApiConverter;

        public ProductConverter() : this(new ProductTypeConverter(), new CategoryConverter(), new CurrencyConverter(), new UnitTypeConverter(), new UserConverter(), new ShopApiConverter())
        {

        }

        public ProductConverter(IProductTypeConverter productTypeConverter, ICategoryConverter categoryConverter, ICurrencyConverter currencyConverter, IUnitTypeConverter unitTypeConverter, IUserConverter userConverter, IShopApiConverter shopApiConverter)
        {
            _productTypeConverter = productTypeConverter;
            _categoryConverter = categoryConverter;
            _currencyConverter = currencyConverter;
            _unitTypeConverter = unitTypeConverter;
            _userConverter = userConverter;
            _shopApiConverter = shopApiConverter;
        }

        public Product ConvertDtoToDB(ProductDto dto)
        {
            if (dto == null) return null;
            return new Product
            {
                Id = dto.Id,
                Timestamp = dto.Timestamp,
                ProductType_Id = dto.ProductType.Id,
                ProductType = _productTypeConverter.ConvertDtoToDB(dto.ProductType),
            };
        }

        public ProductDto ConvertDBToDto(Product entry)
        {
            if (entry == null) return null;
            return new ProductDto
            {
                Id = entry.Id,
                ProductType = _productTypeConverter.ConvertDBToDto(entry.ProductType),
                Timestamp = entry.Timestamp
            };
        }

        public UserProduct ConvertUserProductDtoToDb(UserProductDto dto)
        {
            if (dto == null) return null;
            return new UserProduct
            {
                Id = dto.Id,
                Category_Id = dto.Category.Id,
                Category = _categoryConverter.ConvertDtoToDB(dto.Category),
                Name = dto.Name,
                Currency_Id = dto.Currency.Id,
                Currency = _currencyConverter.ConvertDtoToDB(dto.Currency),
                UnitType_Id = dto.UnitType.Id,
                UnitType = _unitTypeConverter.ConvertDtoToDB(dto.UnitType),
                Price = dto.Price,
                User_Id = dto.User.Id,
                User = _userConverter.ConvertDtoToDB(dto.User)
                // TODO Product = new Product {}
            };
        }

        public UserProductDto ConvertUserProductToUserProductDto(UserProduct product)
        {
            if (product == null) return null;
            return new UserProductDto
            {
                Id = product.Id,
                User = _userConverter.ConvertDBToDto(product.User),
                Name = product.Name,
                Currency = _currencyConverter.ConvertDBToDto(product.Currency),
                Category = _categoryConverter.ConvertDBToDto(product.Category),
                Price = product.Price,
                UnitType = _unitTypeConverter.ConvertDBToDto(product.UnitType)
                // TODO ProductType???
            };
        }

        public ApiProduct ConvertApiProductDtoToDb(ApiProductDto dto)
        {
            if (dto == null) return null;
            return new ApiProduct
            {
                Id = dto.Id,
                ShopApi = _shopApiConverter.ConvertDtoToDB(dto.ShopApi),
                ShopApi_Id = dto.ShopApi.Id,
                Currency_Id = dto.Currency.Id,
                Currency = _currencyConverter.ConvertDtoToDB(dto.Currency),
                Unit_Id = dto.UnitType.Id,
                UnitType = _unitTypeConverter.ConvertDtoToDB(dto.UnitType),
                Price = dto.Price,
                DefaultProduct = ConvertDefaultProductDtoToDb(dto.DefaultProduct),
                DefaultProduct_Id = dto.DefaultProduct.Id,
                Endpoint = dto.Endpoint,
                Quantity = dto.Quantity
                // TODO ???
            };
        }

        public ApiProductDto ConvertApiProductDbToDto(ApiProduct product)
        {
            if (product == null) return null;
            return new ApiProductDto
            {
                Id = product.Id,
                Price = product.Price,
                Currency = _currencyConverter.ConvertDBToDto(product.Currency),
                UnitType = _unitTypeConverter.ConvertDBToDto(product.UnitType),
                ShopApi = _shopApiConverter.ConvertDBToDto(product.ShopApi),
                DefaultProduct = ConvertDefaultProductDbToDto(product.DefaultProduct),
                Endpoint = product.Endpoint,
                Quantity = product.Quantity
                // TODO NAME?
            };
        }

        public DefaultProductDto ConvertDefaultProductDbToDto(DefaultProduct product)
        {
            if (product == null) return null;
            return new DefaultProductDto
            {
                Id = product.Id,
                Price = product.Price,
                Currency = _currencyConverter.ConvertDBToDto(product.Currency),
                UnitType = _unitTypeConverter.ConvertDBToDto(product.UnitType)
                // TODO QUANTITY, CATEGORY, NAME?
            };
        }

        public DefaultProduct ConvertDefaultProductDtoToDb(DefaultProductDto product)
        {
            if (product == null) return null;
            return new DefaultProduct
            {
                Id = product.Id,
                Currency = _currencyConverter.ConvertDtoToDB(product.Currency),
                Currency_Id = product.Currency.Id,
                Price = product.Price,
                UnitType = _unitTypeConverter.ConvertDtoToDB(product.UnitType),
                UnitType_Id = product.UnitType.Id
                // TODO Price nullable or not?
            };
        }
    }
}