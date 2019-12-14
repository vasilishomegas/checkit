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
using ListIt_BusinessLogic.Services.Converters;
using ListIt_BusinessLogic.Services.Converters.Interface;
using ListIt_DataAccess.Repository.Interface;

namespace ListIt_BusinessLogic.Services
{
    public class ShoppingListService : Service<ShoppingList, ShoppingListDto>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListConverter _shoppingListConverter;

        public ShoppingListService(IShoppingListRepository shoppingListRepository, IShoppingListConverter shoppingListConverter) : base(shoppingListRepository, shoppingListConverter)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListConverter = shoppingListConverter;
        }

        public override void Create(ShoppingListDto dto)
        {
            var list = new ShoppingList
            {
                Id = dto.Id,
                Name = dto.Name,
                Path = dto.Path,
                Timestamp = DateTime.Now,
                ChosenSorting_Id = dto.ChosenSortingId
            };

            /*
            var link = new LinkUserToList
            {
                UserId = dto.UserId,
                ShoppingListId = dto.Id,
                ListAccessTypeId = dto.ListAccessTypeId,
            }; */

            _shoppingListRepository.Create(list);
        }

        public override void Update(ShoppingListDto dto)
        {
            var dbList = _shoppingListRepository.Get(dto.Id);
            if (dbList == null) throw new KeyNotFoundException("No list with this id found");

            if (dto.Name == null) dto.Name = dbList.Name;
            if (dto.Path == null) dto.Path = dbList.Path;
            if (dto.ChosenSortingId == 0) dto.ChosenSortingId = dbList.ChosenSorting_Id;
            

            _shoppingListRepository.Update(new ShoppingList
            {
                Id = dto.Id,
                Name = dto.Name,
                Path = dto.Path,
                Timestamp = dbList.Timestamp,
                ChosenSorting_Id = dto.ChosenSortingId
            });
        }
 
        /*
        public IList<ShoppingListDto> GetListsByUserId(int userId)
        {
            var linkedUserLists = _shoppingListRepository.GetLinkByUserId(userId);
            List<ShoppingListDto> listOfLists = new List<ShoppingListDto>();

            foreach(LinkUserToList link in linkedUserLists)
            {
                var list = Get(link.ShoppingListId);
                listOfLists.Add(list);
            }

            return listOfLists;
        }
        */

        /* moved to ShoppingListConverter
        public static ShoppingList StaticDtoToDB(ShoppingListDto listDto)
        {
            //  AS SOON AS THERE IS A ShoppingList CREATED THERE NEEDS ALSO TO BE 
            //  A LinkUserToList ENTRY CREATED

            StaticListDtoToLinkDB(listDto); //Adding entry in LinkUserToList still needs to be added in Repository

            return new ShoppingList
            {
                Id = listDto.Id,
                Name = listDto.Name,
                Path = "whatever???",
                Timestamp = DateTime.Now,
                ChosenSorting_Id = null,
            };
        }



        // Probably not this way
        public static LinkUserToList StaticListDtoToLinkDB(ShoppingListDto dto)
        {
            return new LinkUserToList
            {
                UserId = dto.UserId,
                ShoppingListId = dto.Id,
                ListAccessTypeId = dto.ListAccessTypeId,
            };
        }
        
        */

    }
}
