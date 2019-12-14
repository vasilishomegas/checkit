using System.Collections.Generic;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Interface
{
    public interface IUserService : IService<User, UserDto>
    {
        void Create(UserDto userDto);
        void Update(UserDto userDto);
        IEnumerable<UserDto> GetAll();
        UserDto Get(int id);
        void Delete(int id);
    }
}
