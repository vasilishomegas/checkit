using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ListIt_DataAccess.Repository.Generics
{
    public class Repository<T> : IRepository<T> where T : class
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
                context.Set<T>().Add(entity);
            }
        }

        public void Update(T entity)
        {
            using (var context = new ListItContext())
            {
                context.Set<T>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (var context = new ListItContext())
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new ListItContext())
            {
                var entity = context.Set<T>().Find(id);
                if (entity == null) return;
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }
    }
}