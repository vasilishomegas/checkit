using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccess.Repository.Interface;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly Func<DbContext> _dbContextFactory;

        public UserRepository()
        {
            _dbContextFactory = () => new ListItContext();
        }

        public UserRepository(Func<DbContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public override IEnumerable<User> GetAll()
        {
            using (var context = _dbContextFactory())
            {
                return context.Set<User>()
                    .Include(x => x.Language)
                    .Include(x => x.Country)
                    .ToList();
            }
        }

        public override User Get(int id)
        {
            using (var context = _dbContextFactory())
            {
                return context.Set<User>()
                    .Include(x => x.Language)
                    .Include(x => x.Country)
                    .SingleOrDefault(x => x.Id == id);
            }
        }

        public User GetUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            using (var context = _dbContextFactory())
            {
                return context.Set<User>()
                    .Include(x => x.Language)
                    .Include(x => x.Country)
                    .SingleOrDefault(x => x.Email == email && x.PasswordHash == passwordHash);
                //   return context.Users.SingleOrDefault(x => x.Email == email && x.PasswordHash == passwordHash);
            }
        }

    }
}