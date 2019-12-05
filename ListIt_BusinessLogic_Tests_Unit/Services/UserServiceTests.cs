using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_DataAccess.Repository;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainInterface.Interfaces.Repository;
using ListIt_DomainModel.DTO;
using Moq;
using NUnit.Framework;

namespace ListIt_BusinessLogic_Tests_Unit.Services
{
    public class UserServiceTests
    {
        [Test]
        public void Create_LanguageIsNull_ExceptionThrown()
        {
            var userRepository = MockUserRepository.GetMock();
            var userConverter = MockUserConverter.GetMock();
            var languageRepository = MockLanguageRepository.GetMock();
            var countryRepository = MockCountryRepository.GetMock();
            var userService = new UserService(userRepository.Object, userConverter.Object, countryRepository.Object, languageRepository.Object);
            
            var userDto = new UserDto()
            {
                Id = 1,
                Country = new CountryDto(),
                Language = null,
                Email = "Sample email",
                Timestamp = new DateTime(2015, 1, 1),
                Nickname = "Sample nickname",
                PasswordHash = "Sample hash"
            };

            Assert.Throws<Exception>(() => userService.Create(userDto));
        }

        [Test]
        public void Create_CountryIsNull_ExceptionThrown()
        {
            var userRepository = MockUserRepository.GetMock();
            var userConverter = MockUserConverter.GetMock();
            var languageRepository = MockLanguageRepository.GetMock();
            var countryRepository = MockCountryRepository.GetMock();
            var userService = new UserService(userRepository.Object, userConverter.Object, countryRepository.Object, languageRepository.Object);

            var userDto = new UserDto()
            {
                Id = 1,
                Country = null,
                Language = new LanguageDto(),
                Email = "Sample email",
                Timestamp = new DateTime(2015, 1, 1),
                Nickname = "Sample nickname",
                PasswordHash = "Sample hash"
            };

            Assert.Throws<Exception>(() => userService.Create(userDto));
        }

        [Test]
        public void Create_CountryAndLanguageAreInstantiated_Created()
        {
            var userRepository = MockUserRepository.GetMock();
            var userConverter = MockUserConverter.GetMock();
            var languageRepository = MockLanguageRepository.GetMock();
            var countryRepository = MockCountryRepository.GetMock();
            var userService = new UserService(userRepository.Object, userConverter.Object, countryRepository.Object, languageRepository.Object);

            var userDto = new UserDto()
            {
                Id = 1,
                Country = new CountryDto(),
                Language = new LanguageDto(),
                Email = "Sample email",
                Timestamp = new DateTime(2015, 1, 1),
                Nickname = "Sample nickname",
                PasswordHash = "Sample hash"
            };

            userService.Create(userDto);

            userRepository.Verify(r => r.Create(It.IsAny<User>()), Times.Once);
        }
    }

    internal abstract class MockUserRepository
    {
        public static Mock<IUserRepository> GetMock()
        {
            var userRepository = new Mock<IUserRepository>();
            return userRepository;
        }
    }

    internal abstract class MockUserConverter
    {
        public static Mock<IUserConverter> GetMock()
        {
            var userConverter = new Mock<IUserConverter>();
            return userConverter;
        }
    }

    internal abstract class MockCountryRepository
    {
        public static Mock<ICountryRepository> GetMock()
        {
            var countryRepository = new Mock<ICountryRepository>();
            countryRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(new Country());
            return countryRepository;
        }
    }

    internal abstract class MockLanguageRepository
    {
        public static Mock<ILanguageRepository> GetMock()
        {
            var languageRepository = new Mock<ILanguageRepository>();
            languageRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(new Language());
            return languageRepository;
        }
    }
}
