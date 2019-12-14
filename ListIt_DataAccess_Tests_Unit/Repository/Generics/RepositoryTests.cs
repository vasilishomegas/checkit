using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DataAccess;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using Moq;
using NUnit.Framework;

namespace ListIt_DataAccess_Tests_Unit.Repository.Generics
{
    [TestFixture(typeof(User))]
    /*
    [TestFixture(typeof(Product))]
    [TestFixture(typeof(Shop))]
    [TestFixture(typeof(ShopApi))]
    [TestFixture(typeof(ShoppingListEntry))]
    [TestFixture(typeof(DefaultProduct))]
    */
    public class RepositoryTests<T>
    where T : class, new()
    {

        [Test]
        public void Get_ShouldGetOne([Range(0, 2)] int id)
        {

            var dbSet = MockDbSetFactory();
            dbSet.Setup(x => x.Find(It.IsAny<int>())).Returns(new T());
            var dbContext = MockDbContextFactory(dbSet);
            var repository = new Repository<T>(() => dbContext.Object);

            var result = repository.Get(id);

            Assert.IsNotNull(result);
            dbSet.Verify(x => x.Find(id), Times.Once);
        }

        [Test]
        public void Create_ShouldBeCreated()
        {
            var dbSet = MockDbSetFactory();
            var dbContext = MockDbContextFactory(dbSet);
            var entity = new T();
            var repository = new Repository<T>(() => dbContext.Object);

            repository.Create(entity);

            dbSet.Verify(x => x.Add(entity), Times.Once);
            dbContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void Update_ShouldBeUpdated()
        {
            var dbSet = MockDbSetFactory();
            var dbContext = MockDbContextFactory(dbSet);
            var entity = new T();
            var repository = new Repository<T>(() => dbContext.Object);

            repository.Update(entity);

            dbSet.Verify(x => x.Attach(entity), Times.Once);
            dbContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void Delete_ShouldBeDeleted([Range(0, 2)]int id)
        {
            var dbSet = MockDbSetFactory();
            var dbContext = MockDbContextFactory(dbSet);
            var repository = new Repository<T>(() => dbContext.Object);

            repository.Delete(id);

            dbSet.Verify(x => x.Remove(It.IsAny<T>()), Times.Once);
            dbContext.Verify(x => x.SaveChanges(), Times.Once);
        }


        private static List<T> GetListOfElements(int elements)
        {
            var entities = new List<T>();
            for (var i = 0; i < elements; i++)
            {
                entities.Add(new T());
            }

            return entities;
        }

        private static Mock<DbSet<T>> MockDbSetFactory()
        {
            var dbSet = new Mock<DbSet<T>>();
            return dbSet;
        }

        private static Mock<ListItContext> MockDbContextFactory(IMock<DbSet<T>> dbSet)
        {
            var context = new Mock<ListItContext>();

            context.Setup(x => x.Set<T>()).Returns(dbSet.Object);

            return context;
        }
    }
}
