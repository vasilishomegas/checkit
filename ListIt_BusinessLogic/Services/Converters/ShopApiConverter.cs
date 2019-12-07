using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services.Converters
{
    public class ShopApiConverter : IDtoDbConverter<ShopApi, ShopApiDto>, IShopApiConverter
    {
        public ShopApiDto ConvertDBToDto(ShopApi shopApi)
        {
            if (shopApi == null) return null;

            return new ShopApiDto
            {
                Id = shopApi.Id,
                Url = shopApi.Url
            };
        }

        public ShopApi ConvertDtoToDB(ShopApiDto shopApiDto)
        {
            if (shopApiDto == null) return null;

            return new ShopApi
            {
                Id = shopApiDto.Id,
                Url = shopApiDto.Url
            };
        }
    }
}
