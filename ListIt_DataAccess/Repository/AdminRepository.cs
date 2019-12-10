using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class AdminRepository : Repository<Admin>
    {
        public override IEnumerable<Admin> GetAll()
        {
            using(var context = new ListItContext())
            {
                return context.Admins
                    .ToList();
            }
        }
        public Admin Get(string username, string password)
        {
            using(var context = new ListItContext())
            {
                return context.Admins.SingleOrDefault(x => x.username == username && x.password == password);
            }
        }
    }
}