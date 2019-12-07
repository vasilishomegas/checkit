using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Converters;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainInterface.Interfaces.Repository;
using ListIt_DomainModel;
using ListIt_DomainModel.DTO;
using Microsoft.CSharp;

namespace ListIt_BusinessLogic.Services
{
    public class ShopApiService : Service<ShopApi, ShopApiDto>
    {
        private readonly IShopApiRepository _shopApiRepository;
        private readonly IShopApiConverter _shopApiConverter;

        public ShopApiService() : this(new ShopApiRepository(), new ShopApiConverter())
        {

        }

        public ShopApiService(IShopApiRepository shopApiRepository, IShopApiConverter shopApiConverter) : base(shopApiRepository, shopApiConverter)
        {
            _shopApiRepository = shopApiRepository;
            _shopApiConverter = shopApiConverter;
        }
    }
}