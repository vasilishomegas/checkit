using System.Linq;

namespace ListIt_DataAccess.Repository.Generics
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);

    }
}