using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DomainModel;
using System.Data.Entity;

namespace ListIt_DataAccess.Repository
{
    public class ChainRepository : Repository<Chain>
    {
        // THIS STUPID GET METHODS ARE NECESSARY FOR EAGER LOADING, WHICH MEANS THAT RELATED CLASSES ARE ALSO RETURNED IN WEB API
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