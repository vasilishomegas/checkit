using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.DTO;
using ListIt_DataAccess.Repository.Generics;

namespace ListIt_BusinessLogic.Services.Generics
{
    public abstract class Service<T, DTO> 
        where T : class
        where DTO : class
    {
        protected Repository<T> _repository;

        protected Service()
        {
            _repository = new Repository<T>();
        }

        public virtual IEnumerable<DTO> GetAll()
        {
            return _repository.GetAll().Select(ConvertDomainToDto).ToList();
        }
        public virtual DTO Get(int id)
        {
            var entity = _repository.Get(id);

            return entity == null
                ? null
                : ConvertDomainToDto(entity);
        }
        public virtual void Create(DTO dto)
        {
            _repository.Create(ConvertDtoToDomain(dto));
        }

        public virtual void Update(DTO dto)
        {
            _repository.Update(ConvertDtoToDomain(dto));
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

        protected abstract T ConvertDtoToDomain(DTO dto);
        protected abstract DTO ConvertDomainToDto(T entity);
    }
}
