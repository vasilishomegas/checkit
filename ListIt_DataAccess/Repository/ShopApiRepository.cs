using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class ShopApiRepository : Repository<ShopApi>
    {
        public override void Delete(int id)
        {
            using (var context = new ListItContext())
            {
                // Find a wanted ShopApi, if it does not exist, throw an exception.
                var shopApi = context.ShopApis.Find(id);
                if (shopApi == null) throw new KeyNotFoundException("No entries were affected - the row does not exist");

                // Clean up references to the deleted ShopApi.
                foreach (var entry in context.Chains.Where(chain => chain.ShopApi_Id == shopApi.Id))
                {
                    entry.ShopApi = null;
                }

                // After there is no references anymore to the ShopApi, try to delete it.
                try
                {
                    context.ShopApis.Remove(shopApi);
                }
                catch (System.ArgumentNullException e)
                {
                    throw new KeyNotFoundException("No entries were affected. " + e.Message);
                }

                // Save changes: commits the transaction.
                context.SaveChanges();
            }
        }
    }
}