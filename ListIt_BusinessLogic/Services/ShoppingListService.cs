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

        public override void Create(ShoppingListDto dto)
        {
            _repository.Create(new ShoppingList
            {
                //assign values here
            });
        }

        public override void Update(ShoppingListDto dto)
        {
            _repository.Update(new ShoppingList
            {
                //assign values here
            });
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
            //dto.ListAccessTypeId should go to LinkUserToList-Table/Class 

            return new ShoppingList
            {
                Id = dto.Id,
                Name = dto.Name,
                Path = "whatever???",
                Timestamp = DateTime.Now,
                ChosenSorting_Id = null,
            };
        }

        public static ShoppingListDto StaticDomainToDto(ShoppingList list)
        {
            return new ShoppingListDto
            {
                Id = list.Id,
                Name = list.Name,
                Path = list.Path,
                //ChosenSortingId = null, //list.ChosenSorting_Id, NULLABLE in ShoppingList
                //ListAccessTypeId = SomeService.SomeFunction???
            };
        }
    }
}
