using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class ShoppingListDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<ShoppingListEntryDto> Entries { get; set; }
        public int ChosenSortingId { get; set; }

        //FROM LinkUserToList.cs in DataAccessModels:
        public int ListAccessTypeId { get; set; }
    }
}