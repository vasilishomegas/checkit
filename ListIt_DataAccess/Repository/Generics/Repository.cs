using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ListIt_DataAccess.Repository.Generics
{
    public abstract class Repository<T> where T : class
    {
        public virtual IEnumerable<T> GetAll()
        {
            // Using disposable context. Dispose the ListItContext after the scope is executed.
            using (var context = new ListItContext())
            {
                // Get a set of generic type T and convert it to a list (IEnumerable<T>).
                return context.Set<T>().ToList();
            }
        }
        
        public virtual T Get(int id)
        {
            using (var context = new ListItContext())
            {
                // Find an element with a relevant ID.
                return context.Set<T>().Find(id);
            }
        }

        public virtual void Create(T entity)
        {
            // Using disposable context. Dispose the ListItContext after the scope is executed.
            using (var context = new ListItContext())
            {
                // Add an entity.
                context.Set<T>().Add(entity);
                try
                {
                    // Try to save changes to a database.
                    context.SaveChanges();
                }
                // If validation does not allow you to save your changes:
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    // String Builder to append messages when iterating through exceptions.
                    StringBuilder builder = new StringBuilder();

                    // For each entity validation error...
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

                    // At the end, throw an exception as a result of method execution, so the above layers are
                    // informed, that something went wrong.
                    throw new Exception(builder.ToString());
                }
            }
        }

        public virtual void Update(T entity)
        {
            // Using disposable context. Dispose the ListItContext after the scope is executed.
            using (var context = new ListItContext())
            {
                // To the set of T, attach an existing object. From now, it will be managed by ListItContext.
                context.Set<T>().Attach(entity);

                // Mark the attached object as modified, so when saving changes, context knows that it has to override
                // columns for an object with such an ID.
                context.Entry(entity).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
                {
                    // Throw an exception not of Entity Framework library, so it can be handled by layers above.
                    throw new KeyNotFoundException(e.Message, e.InnerException);
                }
            }
        }

        public virtual void Delete(int id)
        {
            // Using disposable context. Dispose the ListItContext after the scope is executed.
            using (var context = new ListItContext())
            {
                var entity = context.Set<T>().Find(id);
                try
                {
                    // Remove the entity from the database.
                    context.Set<T>().Remove(entity);
                }
                catch (System.ArgumentNullException e)
                {
                    throw new KeyNotFoundException("No entries were affected; the row does not exist." + e.Message);
                }

                // Made changes will be saved now, or, if something went wrong, the changes will be rolled back.
                context.SaveChanges();
            }
        }

        

    }
}