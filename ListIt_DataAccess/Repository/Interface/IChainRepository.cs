using System.Collections.Generic;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository.Interface
{
    public interface IChainRepository : IRepository<Chain>
    {
        IEnumerable<Chain> GetAll();
        Chain Get(int id);
    }
}
