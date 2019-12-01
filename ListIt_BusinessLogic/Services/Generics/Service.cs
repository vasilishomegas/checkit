using System.Collections.Generic;
using System.Linq;
using ListIt_DataAccess.Repository.Generics;

namespace ListIt_BusinessLogic.Services.Generics
{
    public abstract class Service<T, DTO> 
        where T : class
        where DTO : class
    {
        // Generic repository field.
        protected Repository<T> _repository;

        // Insert an instance through the constructor.
        protected Service(Repository<T> repository)
        {
            _repository = repository;
        }

        public virtual IEnumerable<DTO> GetAll()
        {
            // Get all entities. In select statement specify which data you want to retrieve.
            // Here I gave a reference to a method which specifies what and how will be returned.
            return _repository.GetAll().Select(ConvertDamToDto).ToList();
        }

        public virtual DTO Get(int id)
        {
            var entity = _repository.Get(id);

            // If entity equals null, then return null, otherwise return converted into DTO object.
            return entity == null
                ? null
                : ConvertDamToDto(entity);
        }
        
        // When you want to create an object, you create a simple DTO object. Service layer converts it into
        // Data Access Model.
        public virtual void Create(DTO dto)
        {
            _repository.Create(ConvertDtoToDam(dto));
        }

        // You update with a simple DTO object. Service layer converts it into Data Access Model.
        public virtual void Update(DTO dto)
        {
            _repository.Update(ConvertDtoToDam(dto));
        }

        public virtual void Delete(int id)
        {
            // You can easily call delete method on repository class. All the logic about database connectivity
            // is included into Data Access Layer.
            _repository.Delete(id);
        }

        // Each of service subclasses will specify rules of converting Data Transfer Object
        // into Data Access Model. Methods are protected so they can be overriden in subclasses.
        protected abstract T ConvertDtoToDam(DTO dto);
        protected abstract DTO ConvertDamToDto(T entity);
    }
}
