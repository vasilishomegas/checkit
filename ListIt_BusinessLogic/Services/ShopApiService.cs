﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainModel;
using ListIt_DomainModel.DTO;
using Microsoft.CSharp;

namespace ListIt_BusinessLogic.Services
{
    public class ShopApiService : Service<ShopApi, ShopApiDto>
    {
        public ShopApiService() : base(new ShopApiRepository())
        {

        }
        // Overriden methods are created to move the logic to generic Service class
        // Static methods are needed to be able to convert reference properties in other services
        public static ShopApi StaticDtoToDomain(ShopApiDto shopApiDto)
        {
            if (shopApiDto == null) return null;

            return new ShopApi
            {
                Id = shopApiDto.Id,
                Url = shopApiDto.Url
            };
        }

        public static ShopApiDto StaticDomainToDto(ShopApi shopApi)
        {
            if (shopApi == null) return null;

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