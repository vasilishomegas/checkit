using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using System.Data.Entity;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class ChainRepository : Repository<Chain>
    { 
        public override IEnumerable<Chain> GetAll()
        {
            using (var context = new ListItContext())
            {
                // Load the reference type ShopApi.
                return context.Chains.Include(x => x.ShopApi).ToList();
            }
        }

        public override Chain Get(int id)
        {
            using (var context = new ListItContext())
            {
                // Load the reference type ShopApi and get first element (or null/0) which has the same ID as demanded.
                return context.Chains.Include(x => x.ShopApi).FirstOrDefault(x => x.Id == id);
            }
        }
    }
}