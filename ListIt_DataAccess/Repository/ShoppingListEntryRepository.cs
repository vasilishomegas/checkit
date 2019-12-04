using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class ShoppingListEntryRepository : Repository<ShoppingListEntry>
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

        public new Product Get(int id)
        {
            using (var context = new ListItContext())
            {
                return context.Set<Product>().Find(id);
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

        public int CreateProduct(Product product)
        {
            using (var context = new ListItContext())
            {
                var prod = context.Set<Product>().Add(product);

                try
                {
                    context.SaveChanges();
                    return prod.Id;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        builder.Append("Entity of type " + eve.Entry.Entity.GetType().Name
                                                         + " in state " + eve.Entry.State + " has the following" +
                                                         " validation errors:");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            builder.Append("Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage);
                        }
                    }

                    throw new Exception(builder.ToString());
                }
            }
        }

        public void CreateUserProduct(UserProduct product)
        {
            using (var context = new ListItContext())
            {
                var result = context.Set<UserProduct>().Add(product);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        builder.Append("Entity of type " + eve.Entry.Entity.GetType().Name
                                                         + " in state " + eve.Entry.State + " has the following" +
                                                         " validation errors:");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            builder.Append("Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage);
                        }
                    }

                    throw new Exception(builder.ToString());
                }
            }

        }
    }
}