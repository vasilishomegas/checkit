using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccess.Repository.Helpers;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Repository;

namespace ListIt_DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public new void Create(Product product)
        {
            throw new Exception("Choose one of different overloads of the method - you have to use a specific subclass of Product");
        }

        public void Create(Product product, UserProduct subProduct)
        {
            using (var context = new ListItContext())
            {
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
                return context.Set<UserProduct>().ToList();
            }
        }

        public UserProduct GetUserProduct(int id)
        {
            using (var context = new ListItContext())
            {
                return context.Set<UserProduct>().SingleOrDefault(x => x.Id == id);
            }
        }

        public void Update(UserProduct userProduct)
        {
            using (var context = new ListItContext())
            {
                context.Set<UserProduct>().Attach(userProduct);
                context.Entry(userProduct).State = EntityState.Modified;
                ContextManager.SaveChanges(context);
            }
        }
    }
}