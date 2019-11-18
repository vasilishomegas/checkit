using System.Collections.Generic;
using System.Linq;
using ListIt_BusinessLogic.DTO;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_BusinessLogic.Tools;
using ListIt_DataAccess.Repository;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DomainModel;

namespace ListIt_BusinessLogic.Services
{
    public class ChainService : Service<Chain, ChainDto>
    {
        public ChainService() : base(new ChainRepository())
        {

        }

        public override void Create(ChainDto chainDto)
        {
            var shopApiRepository = new ShopApiRepository();

            int? shopApiId = null;
            if (chainDto.ShopApi != null)
            {
                var shopApi = shopApiRepository.Get(chainDto.ShopApi.Id);
                if (shopApi == null)
                {
                    shopApi = ShopApiService.StaticDtoToDomain(chainDto.ShopApi);
                    shopApiRepository.Create(shopApi);
                }
                shopApiId = shopApi.Id;
            }

            _repository.Create(new Chain
            {
                Id = chainDto.Id,
                Logo = chainDto.Logo,
                Name = chainDto.Name,
                ShopApi_Id = shopApiId
                // ShopApi = shopApi    Don't link it because every time it creates a new instance
            });
        }

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


        protected override Chain ConvertDtoToDomain(ChainDto chainDto) 
        {
            int? shopApiId = null;
            ShopApiDto shopApiDto = null;

            if (chainDto.ShopApi != null)
            {
                shopApiId = chainDto.ShopApi.Id;
                shopApiDto = chainDto.ShopApi;
            }

            return new Chain()
            {
                Id = chainDto.Id,
                Logo = chainDto.Logo,
                Name = chainDto.Name,
                ShopApi_Id = shopApiId,
                ShopApi = ShopApiService.StaticDtoToDomain(shopApiDto)
            };

        }

        protected override ChainDto ConvertDomainToDto(Chain chain)
        {
            return new ChainDto
            {
                Id = chain.Id,
                ShopApi = ShopApiService.StaticDomainToDto(chain.ShopApi),
                Name = chain.Name,
                Logo = chain.Logo
            };
        }
    }
}
