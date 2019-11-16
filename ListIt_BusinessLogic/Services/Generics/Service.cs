using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DomainModel;

namespace ListIt_BusinessLogic.Services.Generics
{
    public class Service<T> where T : class
    {
        protected readonly Repository<T> _repository = new Repository<T>();

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T Get(int id)
        {
            return _repository.Get(id);
        }

        public void Create(T entity)
        {
            _repository.Create(entity);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
