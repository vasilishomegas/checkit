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
        protected readonly Repository<T> _repository = new Repository<T>();

        public IEnumerable<DTO> GetAll()
        {
            return _repository.GetAll().Select(ConvertDomainToDto).ToList();
        }
        public DTO Get(int id)
        {
            var entity = _repository.Get(id);

            return entity == null
                ? null
                : ConvertDomainToDto(entity);
        }
        public void Create(DTO dto)
        {
            _repository.Create(ConvertDtoToDomain(dto));
        }

        public void Update(DTO dto)
        {
            _repository.Update(ConvertDtoToDomain(dto));
        }

        public void Delete(int id)
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
