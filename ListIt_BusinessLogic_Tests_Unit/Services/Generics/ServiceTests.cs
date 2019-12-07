using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainInterface.Interfaces.Repository;
using ListIt_DomainModel.DTO;
using Moq;
using NUnit.Framework;

namespace ListIt_BusinessLogic_Tests_Unit.Services.Generics
{
    // Run test in these variants of T and DTO
    [TestFixture(typeof(User), typeof(UserDto))]
    [TestFixture(typeof(Category), typeof(CategoryDto))]
    [TestFixture(typeof(Chain), typeof(ChainDto))]
    [TestFixture(typeof(Country), typeof(CountryDto))]
    [TestFixture(typeof(Currency), typeof(CurrencyDto))]
    [TestFixture(typeof(Language), typeof(LanguageDto))]

    public class ServiceTests<T, DTO>
        where T : class, new()  // is a reference type and has a constructor
        where DTO : class, new()
    {
        [Test]
        public void GetAll_ShouldReturnTheSameNumberOfElements([Values(0, 1, 2)] int elements)
        {
            // Arrange
            if (elements < 0) elements = 0;
            var repository = RepositoryMockFactory<T>.GetMock(elements);
            var converter = ConvertDBtoDtoFactory<T, DTO>.GetMock();
            var service = new Service<T, DTO>(repository.Object, converter.Object); // Create an instance of tested class.

            // Act
            var result = service.GetAll();

            // Assert
            Assert.AreEqual(elements, result.Count());
        }

        [Test]
        public void Get_ElementExists()
        {
            var repository = RepositoryMockFactory<T>.GetMock(1);
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns(new T());
            var converter = ConvertDBtoDtoFactory<T, DTO>.GetMock();
            var service = new Service<T, DTO>(repository.Object, converter.Object);

            var result = service.Get(1);

            Assert.NotNull(result);
        }

        [Test]
        public void Get_ElementNotExists()
        {
            var repository = RepositoryMockFactory<T>.GetMock(1);
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns(() => null);
            var converter = ConvertDBtoDtoFactory<T, DTO>.GetMock();
            var service = new Service<T, DTO>(repository.Object, converter.Object);

            var result = service.Get(1);

            Assert.Null(result);
        }
    }
}

internal abstract class RepositoryMockFactory<T> where T : class, new()
{
    public static Mock<IDtoToDbConverter<T>> GetMock(int elements)
    {
        // I don't mock data types T and DTO because tests should operate on real data classes

        // Configure the mock of repository. Configure repository.GetAll() method to return list of entities
        // (we don't test repository here, so we assume it returns specific values)
        // Prepare list of entities which I expect to be converted

        var entities = new List<T>(); // 2 elements

        for (var i = 0; i < elements; i++)
        {
            entities.Add(new T());
        }

        var repository = new Mock<IDtoToDbConverter<T>>();
        repository.Setup(r => r.GetAll()).Returns(entities);

        return repository;
    }
}

internal abstract class ConvertDBtoDtoFactory<T, DTO> 
    where T : class, new() 
    where DTO : class, new()
{
    public static Mock<IDtoDbConverter<T, DTO>> GetMock()
    {  
        // Configure the mock of converter. Configure ConvertDBtoDto to return new DTO. We do not care
        // about what is inside T and DTO because we do not test converters here. We just expect a specific return.
        var converter = new Mock<IDtoDbConverter<T, DTO>>();
        converter.Setup(c => c.ConvertDBToDto(It.IsAny<T>())).Returns(new DTO());
        return converter;
    }
}