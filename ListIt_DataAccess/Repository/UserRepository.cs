using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class UserRepository : Repository<User>
    {
        
        public override IEnumerable<User> GetAll()
        {
            using (var context = new ListItContext())
            {
                // Get all users, but include references to Language and Country objects.
                return context.Users
                    .Include(x => x.Language)
                    .Include(x => x.Country)
                    .ToList();
            }
        }

        public override User Get(int id)
        {
            using (var context = new ListItContext())
            {
                // Get a user by Id, lambda predicate to filter and get the demanded one.
                return context.Users
                    .Include(x => x.Language)
                    .Include(x => x.Country)
                    .SingleOrDefault(x => x.Id == id);
            }
        }

        public User GetUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            using (var context = new ListItContext())
            {
                // Get a user by Email and PasswordHash.
                return context.Users
                    .Include(x => x.Language)
                    .Include(x => x.Country)
                    .SingleOrDefault(x => x.Email == email && x.PasswordHash == passwordHash);
            }
        }

    }
}