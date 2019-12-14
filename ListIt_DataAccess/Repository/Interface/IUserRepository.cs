using System.Collections.Generic;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAll();
        User Get(int id);
    }
}
