﻿using System.Collections.Generic;
using System.Linq;
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
                var shopApi = context.ShopApis.Find(id);
                if (shopApi == null) throw new KeyNotFoundException("No entries were affected - the row does not exist");

                foreach (var entry in context.Chains.Where(chain => chain.ShopApi_Id == shopApi.Id))
                {
                    entry.ShopApi = null;
                }

                try
                {
                    context.ShopApis.Remove(shopApi);
                }
                catch (System.ArgumentNullException e)
                {
                    throw new KeyNotFoundException("No entries were affected. " + e.Message);
                }

                context.SaveChanges();
            }
        }
    }
}