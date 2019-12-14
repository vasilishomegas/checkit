using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using ListIt_DataAccess.Repository;
using Moq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using ListIt_DataAccess;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess_Tests_Unit.Repository
{
    public class UserRepositoryTests
    {
        [Test]
        public void Update_ShouldBeUpdated()
        {
            var dbSet = MockDbSetFactory();
            var dbContext = MockDbContextFactory(dbSet);
            var repository = new UserRepository((() => dbContext.Object));

            var user = new User();

            repository.Update(user);

            dbSet.Verify(x => x.Attach(user), Times.Once);
            dbContext.Verify(x => x.SaveChanges(), Times.Once);
        }


        private static Mock<DbSet<User>> MockDbSetFactory()
        {
            return new Mock<DbSet<User>>();
        }

        private static Mock<ListItContext> MockDbContextFactory(IMock<DbSet<User>> dbSet)
        {
            var context = new Mock<ListItContext>();

            context.Setup(x => x.Users).Returns(dbSet.Object);
            context.Setup(x => x.Set<User>()).Returns(dbSet.Object);

            return context;
        }
    }
}
