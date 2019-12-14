using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ListIt_DataAccess.Repository.Helpers;
using ListIt_DataAccess.Repository.Interface;

namespace ListIt_DataAccess.Repository.Generics
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Func<DbContext> _dbContextFactory;

        public Repository(Func<DbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Repository()
        {
            _dbContextFactory = () => new ListItContext();
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var context = _dbContextFactory())
            {
                return context.Set<T>().ToList();
            }
        }
        
        public virtual T Get(int id)
        {
            using (var context = _dbContextFactory())
            {
                return context.Set<T>().Find(id);
            }
        }

        public virtual void Create(T entity)
        {
            using (var context = _dbContextFactory())
            {
                context.Set<T>().Add(entity);
                ContextManager.SaveChanges(context);
            }
        }

        public virtual void Update(T entity)
        {
            using (var context = _dbContextFactory())
            {
                context.Set<T>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                ContextManager.SaveChanges(context);
            }
        }

        public virtual void Delete(int id)
        {
            using (var context = _dbContextFactory())
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