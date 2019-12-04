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
    //SHOULD COMBINE ShoppingList and LinkUserToList DB-Entities

    public class ShoppingListRepository : Repository<ShoppingList>
    {
        public override IEnumerable<ShoppingList> GetAll()
        {
            using (var context = new ListItContext())
            {
                return context.ShoppingLists
                    .Include(x => x.LinkUserToLists)
                    .ToList();
            }
        }

        public IEnumerable<LinkUserToList> GetLinkByUserId(int userId)
        {
            using (var context = new ListItContext())
            {
                return context.LinkUserToLists
                    .Where(x => x.UserId == userId)
                    .ToList();
            }
            
        }

        //getting single link entry
        public LinkUserToList GetLink(int id, int userid)
        {
            using (var context = new ListItContext())
            {
                return context.LinkUserToLists
                    .Where(x => x.ShoppingListId == id)
                    .SingleOrDefault(x => x.UserId == userid);
            }
        }

        //public override ShoppingList Get(int id)
        //{
        //    using (var context = new ListItContext())
        //    {
        //        return context.ShoppingLists
        //            .Include(x => x.LinkUserToLists)
        //            .SingleOrDefault(x => x.Id == id);
        //    }
        //}


        /* UPDATING Create() to save ShoppingList AND LinkUserToList */

        public void Create(ShoppingList entity, LinkUserToList link)
        {
            using (var context = new ListItContext())
            {
                var result = context.ShoppingLists.Add(entity);
                var linkresult = context.LinkUserToLists.Add(link);
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
        public void Create(LinkUserToList link)
        {
            using (var context = new ListItContext())
            {
                var linkresult = context.LinkUserToLists.Add(link);
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

        //public virtual void Update(T entity)
        //{
        //    using (var context = new ListItContext())
        //    {
        //        context.Set<T>().Attach(entity);
        //        context.Entry(entity).State = EntityState.Modified;
        //        try
        //        {
        //            context.SaveChanges();
        //        }
        //        catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
        //        {
        //            throw new KeyNotFoundException(e.Message, e.InnerException);
        //        }
        //    }
        //}

        //public virtual void Delete(int id)
        //{
        //    using (var context = new ListItContext())
        //    {
        //        var entity = context.Set<T>().Find(id);
        //        try
        //        {
        //            context.Set<T>().Remove(entity);
        //        }
        //        catch (System.ArgumentNullException e)
        //        {
        //            throw new KeyNotFoundException("No entries were affected; the row does not exist." + e.Message);
        //        }
        //        context.SaveChanges();
        //    }
        //}
    }
}