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
        protected override Chain ConvertDtoToDomain(ChainDto chainDto)
        {
            return new Chain()
            {
                Id = chainDto.Id,
                Logo = chainDto.Logo,
                Name = chainDto.Name,
                ShopApi_Id = chainDto.ShopApi.Id,
                ShopApi = ShopApiService.StaticDtoToDomain(chainDto.ShopApi)
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
