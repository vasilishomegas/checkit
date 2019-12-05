using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository.Generics;
using Moq;
using NUnit.Framework;

namespace ListIt_BusinessLogic_Tests_Unit.Services.Generics
{
    // https://stackoverflow.com/questions/49659515/resharper-shows-all-tests-as-inconclusive-and-has-error-testadapter-dll-does-no
    public class ServiceTests<T, DTO>
        where T : class, new()
        where DTO : class, new()
    {
        [Test]
        public void GetAll_ShouldReturnTheSameNumberOfElements()
        {
            // Arrange
            var entities = new List<T>() { new T { }, new T { } };
            var repository = new Mock<IRepository<T>>();
            repository.Setup(r => r.GetAll()).Returns(entities);
            var converter = new Mock<IDtoDbConverter<T, DTO>>();
            var service = new Service<T, DTO>(repository.Object, converter.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.Equals(entities.Count, result.Count());
        }

        [Test] 
        public void Check()
        {

        }
    }
}