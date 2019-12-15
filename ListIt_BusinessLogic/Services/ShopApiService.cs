using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class ShopApiService : Service<ShopApi, ShopApiDto>
    {
        public ShopApiService() : base(new ShopApiRepository())
        {

        }
        // Overriden methods are created to move the logic to generic Service class
        // Static methods are needed to be able to convert reference properties in other services
        public static ShopApi StaticDtoToDB(ShopApiDto shopApiDto)
        {
            /* OLD BODY
            int id;
            string url;

            if (shopApiDto != null)
            {
                id = shopApiDto.Id;
                url = shopApiDto.Url;
            }
            else return null;

            return new ShopApi
            {
                Id = id,
                Url = url
            };
            */

            if (shopApiDto == null) return null;

            return new ShopApi
            {
                Id = shopApiDto.Id,
                Url = shopApiDto.Url
            };
        }

        public static ShopApiDto StaticDBToDto(ShopApi shopApi)
        {
            if (shopApi == null) return null;

            return new ShopApiDto
            {
                Id = shopApi.Id,
                Url = shopApi.Url
            };
        }

        protected override ShopApi ConvertDtoToDB(ShopApiDto shopApiDto)
        {
            return StaticDtoToDB(shopApiDto);
        }

        protected override ShopApiDto ConvertDBToDto(ShopApi shopApi)
        {
            return StaticDBToDto(shopApi);
        }
        
    }
}