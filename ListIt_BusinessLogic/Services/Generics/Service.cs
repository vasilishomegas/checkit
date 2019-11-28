using System.Collections.Generic;
using System.Linq;
using ListIt_DataAccess.Repository.Generics;

namespace ListIt_BusinessLogic.Services.Generics
{
    public abstract class Service<T, DTO> 
        where T : class
        where DTO : class
    {
        protected Repository<T> _repository;

        protected Service(Repository<T> repository)
        {
            _repository = repository;
        }

        public virtual IEnumerable<DTO> GetAll()
        {
            return _repository.GetAll().Select(ConvertDBToDto).ToList();
        }
        public virtual DTO Get(int id)
        {
            var entity = _repository.Get(id);

            return entity == null
                ? null
                : ConvertDBToDto(entity);
        }
        public virtual void Create(DTO dto)
        {
            _repository.Create(ConvertDtoToDB(dto));
        }

        public virtual void Update(DTO dto)
        {
            _repository.Update(ConvertDtoToDB(dto));
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
        }

        // =========================================================================
        // =========================================================================

        protected bool CheckIfExists(int id)
        {
            var existing = _repository.Get(id);
            return existing != null;
        }

        protected abstract T ConvertDtoToDB(DTO dto);
        protected abstract DTO ConvertDBToDto(T entity);
    }
}
