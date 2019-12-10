using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class ShopService : Service<Shop, ShopDto>
    {
        public ShopService() : base(new ShopRepository())
        {

        }
        public static Shop StaticDtoToDB(ShopDto shopDto)
        {
            return new Shop
            {
                Id = shopDto.Id,
                Street = shopDto.Street,
                StreetNumber = shopDto.Number,
                Zipcode = shopDto.Zipcode,
                City = shopDto.City,
                Chain_Id = shopDto.Chain_id
            };
        }
        public static ShopDto StaticDBToDto(Shop shop)
        {
            return new ShopDto
            {
                Id = shop.Id,
                Street = shop.Street,
                Number = shop.StreetNumber,
                Zipcode = shop.Zipcode,
                City = shop.City,
                Chain_id = shop.Chain_Id
            };
        }

        protected override Shop ConvertDtoToDB(ShopDto dto)
        {
            return StaticDtoToDB(dto);
        }

        protected override ShopDto ConvertDBToDto(Shop entity)
        {
            throw new NotImplementedException();
        }
    }
}
