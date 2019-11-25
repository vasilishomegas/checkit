using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using System.Security.Cryptography;

namespace ListIt_BusinessLogic.Services
{
    public class ShoppingListService : Service<ShoppingList, ShoppingListDto>
    {
        public ShoppingListService() : base(new ShoppingListRepository())
        {

        }

        protected override ShoppingListDto ConvertDomainToDto(ShoppingList entity)
        {
            return StaticDomainToDto(entity);
        }

        protected override ShoppingList ConvertDtoToDomain(ShoppingListDto dto)
        {
            return StaticDtoToDomain(dto);
        }

        public static ShoppingList StaticDtoToDomain(ShoppingListDto dto)
        {
            return new ShoppingList
            {
                //DB-Attribute = dto.Attribute
            };
        }

        public static ShoppingListDto StaticDomainToDto(ShoppingList list)
        {
            return new ShoppingListDto
            {
                //dtt-Attribute = list.Attribute
            };
        }
    }
}
