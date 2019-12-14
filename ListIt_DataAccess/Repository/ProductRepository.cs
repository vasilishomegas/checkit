using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccess.Repository.Helpers;
using ListIt_DataAccess.Repository.Interface;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public override void Create(Product product)
        {
            throw new Exception("Choose one of different overloads of the method - you have to use a specific subclass of Product");
        }


        public void Create(Product product, UserProduct subProduct)
        {
            using (var context = new ListItContext())
            {
                context.Entry(product.ProductType).State = EntityState.Unchanged;
                context.Entry(subProduct.Currency).State = EntityState.Unchanged;
                context.Entry(subProduct.UnitType).State = EntityState.Unchanged;

                product = context.Set<Product>().Add(product);
                subProduct.Id = product.Id;
                context.Set<UserProduct>().Add(subProduct);

                ContextManager.SaveChanges(context);
            }
        }


        public void Create(Product product, ApiProduct subProduct)
        {
            using (var context = new ListItContext())
            {
                context.Entry(product.ProductType).State = EntityState.Unchanged;
                context.Entry(subProduct.Currency).State = EntityState.Unchanged;
                context.Entry(subProduct.UnitType).State = EntityState.Unchanged;
              
                product = context.Set<Product>().Add(product);
                subProduct.Id = product.Id;
                context.Set<ApiProduct>().Add(subProduct);

                ContextManager.SaveChanges(context);
            }
        }

        public void Create(Product product, DefaultProduct subProduct)
        {
            using (var context = new ListItContext())
            {
                context.Entry(product.ProductType).State = EntityState.Unchanged;
                context.Entry(subProduct.Currency).State = EntityState.Unchanged;
                context.Entry(subProduct.UnitType).State = EntityState.Unchanged;

                product = context.Set<Product>().Add(product);
                subProduct.Id = product.Id;
                context.Set<DefaultProduct>().Add(subProduct);

                ContextManager.SaveChanges(context);
            }
        }

        public IEnumerable<UserProduct> GetAllUserProducts()
        {
            using (var context = new ListItContext())
            {
                return context.Set<UserProduct>()
                    .Include(x => x.Category)
                    .Include(x => x.Currency)
                    .Include(x => x.UnitType)
                    .Include(x => x.User.Country)
                    .Include(x => x.User.Language)
                    .ToList();
            }
        }

        public IEnumerable<ApiProduct> GetAllApiProducts()
        {
            using (var context = new ListItContext())
            {
                return context.Set<ApiProduct>()
                    .Include(x => x.ShopApi)
                    .Include(x => x.Currency)
                    .Include(x => x.DefaultProduct)
                    .Include(x => x.UnitType)
                    .ToList();
            }
        }

        public IEnumerable<DefaultProduct> GetAllDefaultProducts()
        {
            using (var context = new ListItContext())
            {
                return context.Set<DefaultProduct>()
                    .Include(x => x.Currency)
                    .Include(x => x.UnitType)
                    .ToList();
            }
        }

        public UserProduct GetUserProduct(int id)
        {
            using (var context = new ListItContext())
            {
                return context.Set<UserProduct>()
                    .Include(x => x.Category)
                    .Include(x => x.Currency)
                    .Include(x => x.UnitType)
                    .Include(x => x.User.Country)
                    .Include(x => x.User.Language)
                    .SingleOrDefault(x => x.Id == id);
            }
        }

        public DefaultProduct GetDefaultProduct(int id)
        {
            using (var context = new ListItContext())
            {
                return context.Set<DefaultProduct>()
                    .Include(x => x.Currency)
                    .Include(x => x.UnitType)
                    .SingleOrDefault(x => x.Id == id);
            }
        }

        public ApiProduct GetApiProduct(int id)
        {
            using (var context = new ListItContext())
            {
                return context.Set<ApiProduct>()
                    .Include(x => x.ShopApi)
                    .Include(x => x.Currency)
                    .Include(x => x.DefaultProduct)
                    .Include(x => x.UnitType)
                    .SingleOrDefault(x => x.Id == id);
            }
        }

        public void Update(UserProduct product)
        {
            using (var context = new ListItContext())
            {
                context.Set<UserProduct>().Attach(product);
                context.Entry(product).State = EntityState.Modified;
                ContextManager.SaveChanges(context);
            }
        }

        public void Update(ApiProduct product)
        {
            using (var context = new ListItContext())
            {
                context.Set<ApiProduct>().Attach(product);
                context.Entry(product).State = EntityState.Modified;
                ContextManager.SaveChanges(context);
            }
        }

        public void Update(DefaultProduct product)
        {
            using (var context = new ListItContext())
            {
                context.Set<DefaultProduct>().Attach(product);
                context.Entry(product).State = EntityState.Modified;
                ContextManager.SaveChanges(context);
            }
        }

        public void Delete(Product product)
        {
            using(var context = new ListItContext())
            {
                product = context.Set<Product>()
                    .Include(x => x.ProductType)
                    .SingleOrDefault(x => x.Id == product.Id);

                if(product == null) throw new KeyNotFoundException("Product with such ID was not found.");

                if(product.ProductType == null) throw new Exception("Given product does not have specified its type.");

                switch (product.ProductType.Name)
                {
                    case "ApiProduct":
                        var apiProduct = new ApiProduct {Id = product.Id};
                        context.Set<ApiProduct>().Attach(apiProduct);
                        context.Set<ApiProduct>().Remove(apiProduct);
                        context.Set<Product>().Remove(product);
                        ContextManager.SaveChanges(context);
                        break;
                    case "DefaultProduct":
                        var defaultProduct = new DefaultProduct {Id = product.Id};
                        context.Set<DefaultProduct>().Attach(defaultProduct);
                        context.Set<DefaultProduct>().Remove(defaultProduct);
                        context.Set<Product>().Remove(product);
                        ContextManager.SaveChanges(context);
                        break;
                    case "UserProduct":
                        var userProduct = new UserProduct {Id = product.Id};
                        context.Set<UserProduct>().Attach(userProduct);
                        context.Set<UserProduct>().Remove(userProduct);
                        context.Set<Product>().Remove(product);
                        ContextManager.SaveChanges(context);
                        break;
                    default:
                        throw new Exception("ProductType was found (" + product.ProductType.Name +  
                                            "), but ProductRepository class does not know it.");
                }

            }
        }

        public void Delete(UserProduct product)
        {
            using (var context = new ListItContext())
            {
                context.Set<UserProduct>().Attach(product);
                context.Set<UserProduct>().Remove(product);

                var superProduct = new Product {Id = product.Id};
                context.Set<Product>().Attach(superProduct);
                context.Set<Product>().Remove(superProduct);

                ContextManager.SaveChanges(context);
            }
        }

        public void Delete(ApiProduct product)
        {
            using (var context = new ListItContext())
            {
                context.Set<ApiProduct>().Attach(product);
                context.Set<ApiProduct>().Remove(product);

                var superProduct = new Product { Id = product.Id };
                context.Set<Product>().Attach(superProduct);
                context.Set<Product>().Remove(superProduct);

                ContextManager.SaveChanges(context);
            }
        }

        public void Delete(DefaultProduct product)
        {
            using (var context = new ListItContext())
            {
                context.Set<DefaultProduct>().Attach(product);
                context.Set<DefaultProduct>().Remove(product);

                var superProduct = new Product { Id = product.Id };
                context.Set<Product>().Attach(superProduct);
                context.Set<Product>().Remove(superProduct);

                ContextManager.SaveChanges(context);
            }
        }
    }
}