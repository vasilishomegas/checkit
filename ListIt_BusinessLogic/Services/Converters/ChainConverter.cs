using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class ChainConverter : IChainConverter
    {
        private readonly ShopApiConverter _shopApiConverter = new ShopApiConverter();
        public ChainDto ConvertDBToDto(Chain chain)
        {
            if (chain == null) return null;
            return new ChainDto
            {
                Id = chain.Id,
                ShopApi = _shopApiConverter.ConvertDBToDto(chain.ShopApi),
                Name = chain.Name,
                Logo = chain.Logo
            };
        }

        public Chain ConvertDtoToDB(ChainDto chainDto)
        {
            if (chainDto == null) return null;

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
                ShopApi = _shopApiConverter.ConvertDtoToDB(chainDto.ShopApi)
            };
        }
    }
}
