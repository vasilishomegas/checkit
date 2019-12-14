using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccess.Repository.Helpers;
using ListIt_DataAccess.Repository.Interface;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class ShoppingListEntryRepository : Repository<ShoppingListEntry>, IShoppingListEntryRepository
    {
        public override IEnumerable<ShoppingListEntry> GetAll()
        {
            using (var context = new ListItContext())
            {
                return context.ShoppingListEntries
                    .Include(x => x.Product)
                    .ToList();
            }
        }

        public IEnumerable<ShoppingListEntry> GetEntriesByListId(int listId)
        {
            using (var context = new ListItContext())
            {
                return context.ShoppingListEntries
                    .Where(x => x.ShoppingList_Id == listId)
                    .Include(x => x.Product)
                    .ToList();
            }
        }

        //public int GetIdOfProduct(Product product)
        //{
        //    using (var context = new ListItContext())
        //    {
        //        var prod = context.Products
        //            .Where(x => x.ProductType_Id == product.ProductType_Id)
        //            .SingleOrDefault(x => x.Timestamp == product.Timestamp);

        //        return prod.Id;
        //    }
        //}

        /*
        public void CreateProduct(Product product)
        {
            using (var context = new ListItContext())
            {
                var prod = context.Set<Product>().Add(product);

                ContextManager.SaveChanges(context);
            }
        }

        public void CreateUserProduct(UserProduct product)
        {
            using (var context = new ListItContext())
            {
                var result = context.Set<UserProduct>().Add(product);

                ContextManager.SaveChanges(context);
            }

        } */
    }
}