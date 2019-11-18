using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.DTO;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_BusinessLogic.Tools;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DomainModel;
using Microsoft.CSharp;

namespace ListIt_BusinessLogic.Services
{
    public class ShopApiService : Service<ShopApi, ShopApiDto>
    {
        // Overriden methods are created to move the logic to generic Service class
        // Static methods are needed to be able to convert reference properties in other services
        public static ShopApi StaticDtoToDomain(ShopApiDto shopApiDto)
        {
            return new ShopApi
            {
                Id = shopApiDto.Id,
                Url = shopApiDto.Url
            };
        }

        public static ShopApiDto StaticDomainToDto(ShopApi shopApi)
        {
            return new ShopApiDto
            {
                Id = shopApi.Id,
                Url = shopApi.Url
            };
        }

        protected override ShopApi ConvertDtoToDomain(ShopApiDto shopApiDto)
        {
            return StaticDtoToDomain(shopApiDto);
        }

        protected override ShopApiDto ConvertDomainToDto(ShopApi shopApi)
        {
            return StaticDomainToDto(shopApi);
        }
        
    }
}