using System;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainInterface.Interfaces.Repository;
using ListIt_DomainInterface.Interfaces.Service;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class ChainService : Service<Chain, ChainDto>, IChainService
    {
        private readonly IChainRepository _chainRepository;
        private readonly IShopApiRepository _shopApiRepository;
        private readonly IChainConverter _chainConverter;

        public ChainService(): this(new ChainRepository(), new ChainConverter(), new ShopApiRepository())
        {

        }

        public ChainService(IChainRepository chainRepository, IChainConverter chainConverter, IShopApiRepository shopApiRepository) : base(chainRepository, chainConverter)
        {
            _chainRepository = chainRepository;
            _shopApiRepository = shopApiRepository;
            _chainConverter = chainConverter;
        }


        public override void Create(ChainDto chainDto)
        {
            if (chainDto.Name == null || chainDto.Logo == null)
                throw new Exception("Name and Logo cannot be empty");

            int? shopApiId = null;
            if (chainDto.ShopApi != null)
            {
                var shopApi = _shopApiRepository.Get(chainDto.ShopApi.Id);
                if (shopApi == null)
                {
                    ShopApiConverter shopApiConverter = new ShopApiConverter();

                    shopApi = shopApiConverter.ConvertDtoToDB(chainDto.ShopApi);
                    _shopApiRepository.Create(shopApi);
                }
                shopApiId = shopApi.Id;
            }
            
            _chainRepository.Create(new Chain
            {
                Id = chainDto.Id,
                Logo = chainDto.Logo,
                Name = chainDto.Name,
                ShopApi_Id = shopApiId
                // ShopApi = shopApi    Don't link it because every time it creates a new instance
            });
        }
        /*public override void Create(ChainDto chainDto)
        {

            if (chainDto.Name == null || chainDto.Logo == null)
            throw new Exception("Chain has to have a name and a logo");

            _repository.Create(new Chain
            {
                Name = chainDto.Name,
                Id = chainDto.Id,
                Logo = chainDto.Logo,
                ShopApi_Id = chainDto.ShopApi.Id,
            });
        }*/
        /* CASCADE DELETE - DELETING CHAIN WILL ALSO DELETE ITS SHOPAPI // IMPLEMENTED IN REPOSITORY CLASS
        public override void Delete(int id)
        {
            var shopApiRepository = new Repository<ShopApi>();
            Chain chain = _repository.Get(id);
            ShopApi shopApi = null;

            if(chain != null)
                shopApi = shopApiRepository.Get(chain.Id);
            
            if (shopApi != null)
                shopApiRepository.Delete(shopApi.Id);

            _repository.Delete(id);
        } */
    }
}
