using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class UserListSortingDto : IDto
    {
        public int Id { get; set; }
        public string UserSortingName { get; set; }
        public int ShopId { get; set; }
        public int ShoppingList_Id { get; set; }


    }
}