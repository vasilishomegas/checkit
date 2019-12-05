using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ListIt_DataAccess.Repository.Generics
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public virtual IEnumerable<T> GetAll()
        {
            using (var context = new ListItContext())
            {
                return context.Set<T>().ToList();
            }
        }
        
        public virtual T Get(int id)
        {
            using (var context = new ListItContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public virtual void Create(T entity)
        {
            using (var context = new ListItContext())
            {
                var result = context.Set<T>().Add(entity);
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

        public virtual void Update(T entity)
        {
            using (var context = new ListItContext())
            {
                context.Set<T>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
                {
                    throw new KeyNotFoundException(e.Message, e.InnerException);
                }
            }
        }

        public virtual void Delete(int id)
        {
            using (var context = new ListItContext())
            {
                var entity = context.Set<T>().Find(id);
                try
                {
                    context.Set<T>().Remove(entity);
                }
                catch (System.ArgumentNullException e)
                {
                    throw new KeyNotFoundException("No entries were affected; the row does not exist." + e.Message);
                }
                context.SaveChanges();
            }
        }

        

    }
}