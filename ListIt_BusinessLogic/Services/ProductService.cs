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

namespace ListIt_BusinessLogic.Services
{
    public class ProductService : Service<Product, ProductDto>
    {
        private readonly ProductRepository _prodRepository;

        public ProductService() : base(new ProductRepository())
        {
            _prodRepository = (ProductRepository)_repository;
        }

        #region PRODUCT

        public ProductDto Get(int langId, int id, int listId)
        {
            var product = _repository.Get(id);
            var translation = _prodRepository.GetProductTranslation(langId, id);

            ProductDto productDto = new ProductDto();
            ShoppingListEntryRepository entryRepository = new ShoppingListEntryRepository();
            var entry = entryRepository.GetByProductAndListId(id, listId);

            if (product.ProductType_Id == 3 || product.ProductType_Id == 4) //UserProducts
            {
                var userProduct = _prodRepository.GetUserProduct(id);
                productDto = ConvertUserProductDBToDto(product, userProduct, entry);
            }

            return productDto;
        }

        //create Product of ShoppingListEntryDto
        public new int Create(ProductDto dto)
        {
            return _prodRepository.CreateProduct(new Product
            {
                Id = dto.Id,
                Timestamp = DateTime.Now,
                ProductType_Id = dto.ProductTypeId
            });
        }

        public int GetProductTypeId(int productId)
        {
            return _prodRepository.GetProductTypeId(productId);
        }

        protected override ProductDto ConvertDBToDto(Product entity)
        {
            return StaticDBToDto(entity);
        }

        protected override Product ConvertDtoToDB(ProductDto dto)
        {
            return StaticDtoToDB(dto);
        }
        #endregion PRODUCT

        #region DEFAULTPRODUCT

        public void Create(DefaultProductDto dto)
        {
            var product = new Product
            {
                Id = dto.ProductId,
                Timestamp = DateTime.Now,
                ProductType_Id = dto.ProductTypeId
            };
            var prodId = _prodRepository.CreateProduct(product);

            var defaultProduct = new DefaultProduct
            {
                Id = dto.Id,
                Product_Id = prodId,
                Currency_Id = dto.Currency_Id,
                UnitType_Id = dto.Unit_Id,
                Price = dto.Price,
            };

            _prodRepository.Create(defaultProduct);

            // Link category
        }

        //create Linking for User if DefaultProduct used as entry
        public void CreateLink(DefaultProductDto defaultProduct, int userId)
        {
            var link = _prodRepository.GetLinkUserToDefaultProduct(userId, defaultProduct.Id);
            if(link == null) _prodRepository.Create(ConvertDefaultProductDtoToLinkDB(defaultProduct, userId));
            //else if(link != null) if link already exists do nothing
        }

        public IList<ProductDto> GetDefaultAndReusableProductsByLanguage(int langId)
        {
            var defaultProducts = _prodRepository.GetAllDefaultProducts();
            var reusableProducts = _prodRepository.GetReusableProducts();
            List<ProductDto> products = new List<ProductDto>();

            foreach (DefaultProduct prod in defaultProducts)
            {
                var translation = _prodRepository.GetProductTranslation(langId, (int)prod.Product_Id);

                if(translation == null) //if no translation for this language -> switch to english
                {
                    translation = _prodRepository.GetProductTranslation(2, (int)prod.Product_Id);
                }

                var defProduct = ConvertDBToDto(prod, translation);
                if(defProduct != null) products.Add(defProduct);

            }

            foreach(Product prod in reusableProducts)
            {
                var entry = _prodRepository.GetUserProduct(prod.Id);
                var userProd = ConvertDBToDto(entry);
                if(userProd != null) products.Add(userProd);
            }

            return products;
        }

        public int GetDefaultProductId(int productId)
        {
            return _prodRepository.GetDefaultProductId(productId);
        }

        protected LinkUserToDefaultProduct ConvertDefaultProductDtoToLinkDB(DefaultProductDto defaultProductDto, int userId)
        {
            return StaticDefaultProductDtoToLinkDB(defaultProductDto, userId);
        }

        protected DefaultProductDto ConvertDBToDto(DefaultProduct entity, TranslationOfProduct translation)
        {
            return StaticDBToDto(entity, translation);
        }

        protected DefaultProduct ConvertDtoToDB(DefaultProductDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static LinkUserToDefaultProduct StaticDefaultProductDtoToLinkDB(DefaultProductDto dto, int userId)
        {
            return new LinkUserToDefaultProduct
            {
                UserId = userId,
                DefaultProductId = dto.Id,
                Timestamp = DateTime.Now
            };
        }
        public static DefaultProduct StaticDtoToDB(DefaultProductDto dto)
        {
            return new DefaultProduct
            {
                Product_Id = dto.ProductTypeId,
                //Category_Id = dto.Category_Id,
                Currency_Id = dto.Currency_Id,
                UnitType_Id = dto.Unit_Id,
                //Name = dto.Name,
                Price = dto.Price
            };
        }
        public static DefaultProductDto StaticDBToDto(DefaultProduct defaultProduct, TranslationOfProduct translation)
        {
            if (defaultProduct == null || translation == null) return null;
            return new DefaultProductDto
            {
                Id = defaultProduct.Id,
                Name = translation.Translation,
                Currency_Id = defaultProduct.Currency_Id,
                Unit_Id = defaultProduct.UnitType_Id,
                Price = defaultProduct.Price,
                //Category_Id = defaultProduct.Categories,
                ProductTypeId = 1,	//= DefaultProduct
                ProductId = (int)defaultProduct.Product_Id
            };
        }
        public static Product StaticDtoToDB(ProductDto dto)
        {
            return new Product
            {
                Id = dto.ProductTypeId,
                Timestamp = DateTime.Now,
                ProductType_Id = dto.ProductTypeId,
            };
        }

        public static ProductDto StaticDBToDto(Product product)
        {
            if (product == null) return null;
            return new ProductDto
            {
                Id = product.Id,
                ProductTypeId = product.ProductType_Id
            };
        }

        #endregion DEFAULTPRODUCT

        #region APIPRODUCT

        #endregion APIPRODUCT

        #region USERPRODUCT
        public void Create(UserProductDto dto)
        {
            _prodRepository.Create(StaticDtoToDB(dto));
        }

        public void Update(UserProductDto dto)
        {
            _prodRepository.Update(ConvertDtoToDB(dto));
        }
        public int GetUserProductId(int productId)
        {
            var userProduct = _prodRepository.GetUserProduct(productId);
            return userProduct.Id;
        }

        public void DeleteUserProduct(int id)
        {
            _prodRepository.DeleteUserProduct(id);
        }

        protected UserProduct ConvertDtoToDB(UserProductDto dto)
        {
            return StaticDtoToDB(dto);
        }

        protected UserProductDto ConvertDBToDto(UserProduct userProduct)
        {
            return StaticDBToDto(userProduct);
        }

        public static UserProduct StaticDtoToDB(UserProductDto dto)
        {
            return new UserProduct
            {
                Id = dto.ProductTypeId,
                Category_Id = dto.Category_Id,
                Currency_Id = dto.Currency_Id,
                UnitType_Id = dto.Unit_Id,
                User_Id = dto.User_Id,
                Name = dto.Name,
                Price = dto.Price
            };
        }

        public static UserProductDto StaticDBToDto(UserProduct userProduct)
        {
            if (userProduct == null) return null;
            return new UserProductDto
            {
                Id = userProduct.Id,
                Name = userProduct.Name,
                Currency_Id = (int)userProduct.Currency_Id,
                Unit_Id = (int)userProduct.UnitType_Id,
                ProductId = userProduct.Product_Id,
                //Quantity = from ProductTable
                Price = (int)userProduct.Price,
                Category_Id = userProduct.Category_Id,
                User_Id = userProduct.User_Id,
                //ProductTypeId = from ProductTable
            };
        }

        #endregion USERPRODUCT

        #region SHOPPINGLIST

        //Getting all shoppinglistentries and converting to ProductDto
        public IList<ProductDto> GetEntriesAsProducts(int listId, int langId)
        {
            //need to get Product, ShoppingListEntry and UserProduct for each entry
            List<ProductDto> productDtoList = new List<ProductDto>();

            //1. get ShoppingListEntries
            ShoppingListEntryRepository entryRepository = new ShoppingListEntryRepository();
            var entriesList = entryRepository.GetEntriesByListId(listId);

            foreach (ShoppingListEntry entry in entriesList)
            {
                //2. get Products
                var product = _prodRepository.Get(entry.Product_Id);

                //3. get UserProduct
                if (product.ProductType_Id == 3 || product.ProductType_Id == 4) //UserProducts
                {
                    var userProduct = _prodRepository.GetUserProduct(entry.Product_Id);

                    //4. Create ProductDto and add to list
                    var productDto = ConvertUserProductDBToDto(product, userProduct, entry);
                    productDtoList.Add(productDto);
                }
                else if (product.ProductType_Id == 1) //DefaultProduct
                {
                    var defaultProduct = _prodRepository.GetDefaultProduct(entry.Product_Id);
                    var translation = _prodRepository.GetProductTranslation(langId, entry.Product_Id);

                    //4. Create ProductDto and add to list
                    var productDto = ConvertDefaultProductDBToDto(product, defaultProduct, entry, translation);
                    productDtoList.Add(productDto);
                }
                else if (product.ProductType_Id == 2) //ApiProduct
                {
                    var apiProduct = _prodRepository.GetApiProduct(entry.Product_Id);
                    var translation = _prodRepository.GetProductTranslation(langId, entry.Product_Id);

                    //4. Create ProductDto and add to list
                    var productDto = ConvertApiProductDBToDto(product, apiProduct, entry, translation);
                    productDtoList.Add(productDto);
                }
            }
            return productDtoList;
        }
        public static ProductDto StaticDBToDto(Product product, UserProduct userProduct, ShoppingListEntry entry)
        {
            return new ProductDto
            {
                Id = product.Id,
                ProductTypeId = product.ProductType_Id,
                Name = userProduct.Name,
                Currency_Id = (int)userProduct.Currency_Id,
                Unit_Id = (int)userProduct.UnitType_Id,
                Quantity = (int)entry.Quantity,
                Price = (decimal)userProduct.Price,
                ProductId = product.Id,
                Category_Id = userProduct.Category_Id
            };
        }

        public static ProductDto StaticDBToDto(Product product, DefaultProduct defaultProduct, ShoppingListEntry entry, TranslationOfProduct translation)
        {
            return new ProductDto
            {
                Id = product.Id,
                ProductTypeId = product.ProductType_Id,
                Name = translation.Translation,
                Currency_Id = (int)defaultProduct.Currency_Id,
                Unit_Id = (int)defaultProduct.UnitType_Id,
                Quantity = (int)entry.Quantity,
                Price = (decimal)defaultProduct.Price,
                ProductId = product.Id,
                //Category_Id = defaultProduct.Category_Id
            };
        }

        public static ProductDto StaticDBToDto(Product product, ApiProduct apiProduct, ShoppingListEntry entry, TranslationOfProduct translation)
        {
            return new ProductDto
            {
                Id = product.Id,
                ProductTypeId = product.ProductType_Id,
                Name = translation.Translation,
                Currency_Id = (int)apiProduct.Currency_Id,
                Unit_Id = (int)apiProduct.Unit_Id,
                Quantity = (int)entry.Quantity,
                Price = (decimal)apiProduct.Price,
                ProductId = product.Id,
                //Category_Id = defaultProduct.Category_Id
            };
        }
        protected ProductDto ConvertUserProductDBToDto(Product entity, UserProduct userProduct, ShoppingListEntry entry)
        {
            return StaticDBToDto(entity, userProduct, entry);
        }

        protected ProductDto ConvertDefaultProductDBToDto(Product entity, DefaultProduct defaultProduct, ShoppingListEntry entry, TranslationOfProduct translation)
        {
            return StaticDBToDto(entity, defaultProduct, entry, translation);
        }

        protected ProductDto ConvertApiProductDBToDto(Product entity, ApiProduct apiProduct, ShoppingListEntry entry, TranslationOfProduct translation)
        {
            return StaticDBToDto(entity, apiProduct, entry, translation);
        }

        #endregion SHOPPINGLIST

    }
}
