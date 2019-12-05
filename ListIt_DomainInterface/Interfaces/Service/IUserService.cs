using ListIt_DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListIt_DomainInterface.Interfaces.Service
{
    public interface IUserService : IService<UserDto>
    {
        void Create(UserDto userDto);
        void Update(UserDto userDto);
        IEnumerable<UserDto> GetAll();
        UserDto Get(int id);
        void Delete(int id);
    }
}
