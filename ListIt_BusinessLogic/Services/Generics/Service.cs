using System.Collections.Generic;
using System.Linq;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainInterface.Interfaces.Repository;
using ListIt_DomainInterface.Interfaces.Service;

namespace ListIt_BusinessLogic.Services.Generics
{

    public class Service<T, DTO> : IService<DTO> 
        where T : class
        where DTO : class
    {

        // Here is the problem that I encountered when creating tests:
        // https://stackoverflow.com/questions/243274/how-to-unit-test-abstract-classes-extend-with-stubs
        // Therefore I extracted convert methods to new class.
        // It is a great example why TDD leads to better design.

        protected IRepository<T> _repository;
        protected IDtoDbConverter<T, DTO> _converter;

        public Service(IRepository<T> repository, IDtoDbConverter<T, DTO> converter)
        {
            _repository = repository;
            _converter = converter;
        }

        public virtual IEnumerable<DTO> GetAll()
        {
            return _repository.GetAll().Select(_converter.ConvertDBToDto).ToList();
        }
        public virtual DTO Get(int id)
        {
            var entity = _repository.Get(id);

            return entity == null
                ? null
                : _converter.ConvertDBToDto(entity);
        }
        public virtual void Create(DTO dto)
        {
            _repository.Create(_converter.ConvertDtoToDB(dto));
        }

        public virtual void Update(DTO dto)
        {
            _repository.Update(_converter.ConvertDtoToDB(dto));
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
