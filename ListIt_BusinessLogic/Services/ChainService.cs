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
            var shopApiRepository = new ShopApiRepository();

            int? shopApiId = null;
            shopApiId = chainDto.ShopApi_Id;
            //if (chainDto.ShopApi != null)
            //{
            //    var shopApi = shopApiRepository.Get(chainDto.ShopApi.Id);
            //    if (shopApi == null)
            //    {
            //        shopApi = ShopApiService.StaticDtoToDB(chainDto.ShopApi);
            //        shopApiRepository.Create(shopApi);
            //    }
            //    shopApiId = shopApi.Id;
            //}

            Chain chain = new Chain
            {
                Id = chainDto.Id,
                Logo = chainDto.Logo,
                Name = chainDto.Name
            };
            if (chainDto.ShopApi_Id != 0)
                chain.ShopApi_Id = chainDto.ShopApi_Id;
            _repository.Create(chain);
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


        protected override Chain ConvertDtoToDB(ChainDto chainDto) 
        {
            //int? shopApiId = null;
            //ShopApiDto shopApiDto = null;

            //if (chainDto.ShopApi != null)
            //{
            //    shopApiId = chainDto.ShopApi.Id;
            //    shopApiDto = chainDto.ShopApi;
            //}

            return new Chain()
            {
                Id = chainDto.Id,
                Logo = chainDto.Logo,
                Name = chainDto.Name,
                ShopApi_Id = chainDto.ShopApi_Id,
                //ShopApi = ShopApiService.StaticDtoToDB(shopApiDto)
            };

        }

        protected override ChainDto ConvertDBToDto(Chain chain)
        {
            var chainDto = new ChainDto { 
                Id = chain.Id, 
                Name = chain.Name, 
                Logo = chain.Logo 
            };
            if (chain.ShopApi_Id.HasValue)
                chainDto.ShopApi_Id = (int)chain.ShopApi_Id;
            return chainDto;
        }
    }
}
