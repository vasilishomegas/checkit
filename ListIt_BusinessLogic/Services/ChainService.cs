using System;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class ChainService : Service<Chain, ChainDto>
    {
        public ChainService() : base(new ChainRepository())
        {

        }

        public override void Create(ChainDto chainDto)
        {
            // Chain has a field of ShopApi, so it might be necessary to use ShopApiRepository.
            var shopApiRepository = new ShopApiRepository();

            // Question mark here means that shopApiId can also be null.
            int? shopApiId = null;

            // If ChainDto has assigned ShopApiDto reference...
            if (chainDto.ShopApi != null)
            {
                // Get such a ShopApi from the database
                var shopApi = shopApiRepository.Get(chainDto.ShopApi.Id);

                // If it does not exist yet, create an instance of it in the database.
                if (shopApi == null)
                {
                    shopApi = ShopApiService.StaticDtoToDam(chainDto.ShopApi);
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

        protected override Chain ConvertDtoToDam(ChainDto chainDto) 
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
                ShopApi = ShopApiService.StaticDtoToDam(shopApiDto)
            };

        }

        protected override ChainDto ConvertDamToDto(Chain chain)
        {
            return new ChainDto
            {
                Id = chain.Id,
                ShopApi = ShopApiService.StaticDamToDto(chain.ShopApi),
                Name = chain.Name,
                Logo = chain.Logo
            };
        }

        internal static Chain StaticDtoToDam(ChainDto chain)
        {
            throw new NotImplementedException();
        }

        internal static ChainDto StaticDamToDto(Chain chain)
        {
            throw new NotImplementedException();
        }
    }
}
