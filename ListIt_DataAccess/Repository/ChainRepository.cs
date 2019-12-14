using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using System.Data.Entity;
using ListIt_DataAccess.Repository.Interface;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class ChainRepository : Repository<Chain>, IChainRepository
    {
        // Include methods specify which of reference data has to be loaded
        public override IEnumerable<Chain> GetAll()
        {
            using (var context = new ListItContext())
            {
                return context.Chains.Include(x => x.ShopApi).ToList();
            }
        }

        public override Chain Get(int id)
        {
            using (var context = new ListItContext())
            {
                return context.Chains.Include(x => x.ShopApi).FirstOrDefault(x => x.Id == id);
            }
        }
    }
}