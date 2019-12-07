using ListIt_DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DataAccessModel;

namespace ListIt_DomainInterface.Interfaces.Service
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
