using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DataAccessModel;

namespace ListIt_DomainInterface.Interfaces.Repository
{
    public interface IChainRepository : IRepository<Chain>
    {
        IEnumerable<Chain> GetAll();
        Chain Get(int id);
    }
}
