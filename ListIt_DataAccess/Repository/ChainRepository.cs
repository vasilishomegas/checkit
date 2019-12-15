using System.Collections.Generic;
using System.Linq;
using ListIt_DataAccess.Repository.Generics;
using System.Data.Entity;
using ListIt_DataAccessModel;

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

        public string[] GetShopAndChainNames(int id)
        {
            using (var context = new ListItContext())
            {
                var shop = context.Shops.Find(id);
                var chain = context.Chains.Find(shop.Chain_Id);
                return new string[]
                {
                    chain.Name,
                    shop.City,
                    shop.Street,
                    shop.StreetNumber
                };
            }
        }
    }
}