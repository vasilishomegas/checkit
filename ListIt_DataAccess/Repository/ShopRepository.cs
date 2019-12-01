using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ListIt_DataAccess.Repository
{
    public class ShopRepository : Repository<Shop>
    {
        public override IEnumerable<Shop> GetAll()
        {
            using (var context = new ListItContext())
            {
                return context.Shops
                    .Include(x => x.Chain)
                    .ToList();
            }
        }

        public override Shop Get(int id)
        {
            using (var context = new ListItContext())
            {
                return context.Shops
                    .Include(x => x.Chain)
                    .SingleOrDefault(x => x.Id == id);
            }
        }
    }