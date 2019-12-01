using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DomainModel;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using System.Security.Cryptography;

namespace ListIt_BusinessLogic.Services
{
    public class ShopService : Service<Shop, ShopDto>
    {
        private readonly ShopRepository _shopRepository;
        public ShopService() : base(new ShopRepository())
        {
            _shopRepository = (ShopRepository)_repository;
        }


        public override void Create(ShopDto shopDto)
        {
            var chainRepository = new ChainRepository();

            if (shopDto.Chain == null
               || chainRepository.Get(shopDto.Chain.Id) == null)
                throw new Exception("Chain field cannot be empty");

            _repository.Create(new Shop
            {
                Chain_Id = shopDto.Chain.Id,
                Street = shopDto.Street,
                StreetNumber = shopDto.StreetNumber,
                Zipcode = shopDto.Zipcode,
                City = shopDto.City
            });
        }

        public override void Update(ShopDto shopDto)
        {
            var inDbShop = _repository.Get(shopDto.Id);
            if (inDbShop == null) throw new KeyNotFoundException("No shop with such ID");
            if (shopDto.Street == null) shopDto.Street = inDbShop.Street;
            if (shopDto.StreetNumber == null) shopDto.StreetNumber = inDbShop.StreetNumber;
            if (shopDto.Zipcode == null) shopDto.Zipcode = inDbShop.Zipcode;
            if (shopDto.City == null) shopDto.City = inDbShop.City;
            var newChainId = shopDto.Chain?.Id ?? inDbShop.Chain_Id;

            _repository.Update(new Shop
            {
                Chain_Id = shopDto.Chain.Id,
                Street = shopDto.Street,
                StreetNumber = shopDto.StreetNumber,
                Zipcode = shopDto.Zipcode,
                City = shopDto.City
            });
        }
        protected override ShopDto ConvertDamToDto(Shop entity)
        {
            return StaticDamToDto(entity);
        }

        protected override Shop ConvertDtoToDam(ShopDto dto)
        {
            return StaticDtoToDam(dto);
        }
        // Dam = Data Access Model
        public static Shop StaticDtoToDam(ShopDto shopDto)
        {
            return new Shop
            {
                Chain = ChainService.StaticDtoToDam(shopDto.Chain),
                Chain_Id = shopDto.Chain.Id,
                Id = shopDto.Id,
                Street = shopDto.Street,
                StreetNumber = shopDto.StreetNumber,
                Zipcode = shopDto.Zipcode,
                City = shopDto.City,
            };
        }
        public static ShopDto StaticDamToDto(Shop shop)
        {
            return new ShopDto
            {
                Chain = ChainService.StaticDamToDto(shop.Chain),
                Id = shop.Id,
                Street = shop.Street,
                StreetNumber = shop.StreetNumber,
                Zipcode = shop.Zipcode,
                City = shop.City,
            };
        }
    }
}
