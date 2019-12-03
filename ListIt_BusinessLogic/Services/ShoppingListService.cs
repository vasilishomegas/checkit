﻿using System;
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
        private readonly ShoppingListRepository _listRepository;

        public ShoppingListService() : base(new ShoppingListRepository())
        {
            _listRepository = (ShoppingListRepository)_repository;
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

            var link = new LinkUserToList
            {
                UserId = dto.UserId,
                ShoppingListId = dto.Id,
                ListAccessTypeId = dto.ListAccessTypeId,
            };

            _listRepository.Create(list, link);
        }

        public override void Update(ShoppingListDto dto)
        {
            var dbList = _listRepository.Get(dto.Id);
            if (dbList == null) throw new KeyNotFoundException("No list with this id found");

            if (dto.Name == null) dto.Name = dbList.Name;
            if (dto.Path == null) dto.Path = dbList.Path;
            if (dto.ChosenSortingId == 0) dto.ChosenSortingId = dbList.ChosenSorting_Id;
            

            _repository.Update(new ShoppingList
            {
                Id = dto.Id,
                Name = dto.Name,
                Path = dto.Path,
                Timestamp = dbList.Timestamp,
                ChosenSorting_Id = dto.ChosenSortingId
            });
        }
 
        public IList<ShoppingListDto> GetListsByUserId(int userId)
        {
            var linkedUserLists = _listRepository.GetLinkByUserId(userId);
            List<ShoppingListDto> listOfLists = new List<ShoppingListDto>();

            foreach(LinkUserToList link in linkedUserLists)
            {
                var list = Get(link.ShoppingListId);
                listOfLists.Add(list);
            }

            return listOfLists;
        }

        protected override ShoppingListDto ConvertDBToDto(ShoppingList entity)
        {
            //var link = _listRepository.GetLinkById(entity.Id, );
            //int listAccessTypeId = link.ListAccessTypeId;

            return StaticDBToDto(entity);
        }

        protected override ShoppingList ConvertDtoToDB(ShoppingListDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static ShoppingList StaticDtoToDB(ShoppingListDto listDto)
        {
            //  AS SOON AS THERE IS A ShoppingList CREATED THERE NEEDS ALSO TO BE 
            //  A LinkUserToList ENTRY CREATED

            StaticListDtoToLinkDB(listDto); 

            return new ShoppingList
            {
                Id = listDto.Id,
                Name = listDto.Name,
                Path = "whatever???",
                Timestamp = DateTime.Now,
                ChosenSorting_Id = null,
            };
        }

        public static LinkUserToList StaticListDtoToLinkDB(ShoppingListDto dto)
        {
            return new LinkUserToList
            {
                UserId = dto.UserId,
                ShoppingListId = dto.Id,
                ListAccessTypeId = dto.ListAccessTypeId,
            };
        }

        public static ShoppingListDto StaticDBToDto(ShoppingList list)
        {
            // EACH ShoppingListDto WILL CONTAIN VALUES FROM LinkUserToList AS WELL
            foreach(LinkUserToList link in list.LinkUserToLists)
            {
                
            }
            
            return new ShoppingListDto
            {
                Id = list.Id,
                Name = list.Name,
                Path = list.Path,
                //ChosenSortingId = null, //list.ChosenSorting_Id, NULLABLE in ShoppingList
                //ListAccessTypeId = list.LinkUserToLists
            };
        }

        
    }
}
