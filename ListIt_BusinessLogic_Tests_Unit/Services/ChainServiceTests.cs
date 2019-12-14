using System;
using ListIt_BusinessLogic.Services;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_DataAccess.Repository.Interface;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ListIt_BusinessLogic_Tests_Unit.Services
{
    public class ChainServiceTests
    {

        [Test]
        public void Create_NameIsNull_ExceptionThrown()
        {
            var chainRepository = MockChainRepository.GetMock();
            var chainConverter = MockChainConverter.GetMock();
            var shopApiRepository = MockShopApiRepository.GetMock();
            var chainService = new ChainService(chainRepository.Object, chainConverter.Object, shopApiRepository.Object);

            var chainDto = new ChainDto()
            {
                Id = 1,
                Name = null,
                Logo = "some link lol",
                ShopApi = null
            };

            Assert.Throws<Exception>(() => chainService.Create(chainDto));
        }
        [Test]
        public void Create_LogoIsNotNull_Created()
        {
            var chainRepository = MockChainRepository.GetMock();
            var chainConverter = MockChainConverter.GetMock();
            var shopApiRepository = MockShopApiRepository.GetMock();
            var chainService = new ChainService(chainRepository.Object, chainConverter.Object, shopApiRepository.Object);

            var chainDto = new ChainDto()
            {
                Id = 2,
                Name = "Tesco",
                Logo = "link",
                ShopApi = null
            };

            chainService.Create(chainDto);

            chainRepository.Verify(x => x.Create(It.IsAny<Chain>()), Times.Once);
        }
    }

    internal abstract class MockChainRepository
    {
        public static Mock<IChainRepository> GetMock()
        {
            var chainRepository = new Mock<IChainRepository>();
            return chainRepository;
        }
    }

    internal abstract class MockChainConverter
    {
        public static Mock<IChainConverter> GetMock()
        {
            var chainConverter = new Mock<IChainConverter>();
            return chainConverter;
        }
    }
    internal abstract class MockShopApiRepository
    {
        public static Mock<IShopApiRepository> GetMock()
        {
            var shopApiRepository = new Mock<IShopApiRepository>();
            shopApiRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(new ShopApi());
            return shopApiRepository;
        }
    }
}
