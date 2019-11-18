using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ListIt_DataAccess.Repository.Generics
{
    public class Repository<T> where T : class
    {
        public IEnumerable<T> GetAll()
        {
            using (var context = new ListItContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public T Get(int id)
        {
            using (var context = new ListItContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public void Create(T entity)
        {
            using (var context = new ListItContext())
            {
                var result = context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(T entity)
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

        public void Delete(int id)
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
                    throw new KeyNotFoundException("No entries were affected - the row does not exist");
                }
                context.SaveChanges();
            }
        }
    }
}