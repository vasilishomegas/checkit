using System.Collections.Generic;

namespace ListIt_DomainInterface.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}