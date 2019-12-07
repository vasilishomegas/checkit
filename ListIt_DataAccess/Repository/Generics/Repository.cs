using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ListIt_DataAccess.Repository.Helpers;
using ListIt_DomainInterface.Interfaces.Repository;

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
                ContextManager.SaveChanges(context);
            }
        }

        public virtual void Update(T entity)
        {
            using (var context = new ListItContext())
            {
                context.Set<T>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                ContextManager.SaveChanges(context);
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
                ContextManager.SaveChanges(context);
            }
        }

        

    }
}