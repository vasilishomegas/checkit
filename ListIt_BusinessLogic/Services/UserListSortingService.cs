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
    public class UserListSortingService : Service<UserListSorting, UserListSortingDto>
    {
        private readonly UserListSortingRepository _listSortingRepository;
        private readonly UserEntrySortingRepository _entrySortingRepository;

        public UserListSortingService() : base(new UserListSortingRepository())
        {
            _listSortingRepository = (UserListSortingRepository)_repository;
            _entrySortingRepository = new UserEntrySortingRepository();
        }

        //TODO: (get) apply sorting by id + UserEntrySortings
        public IList<ProductDto> ApplyUserSorting(int sortingId, IList<ProductDto> products)
        {
            List<ProductDto> sortedList = new List<ProductDto>();
            var nextEntry = _entrySortingRepository.GetFirstEntry(sortingId);

            //1. finding very first product
            foreach (ProductDto product in products)
            {
                if (nextEntry.ShoppingListEntry_Id == product.Id) sortedList.Add(product);
            }

            //2. getting each next product
            if(products.Count > 1)
            {
                for (int x = 0; x < products.Count(); x++)
                {
                    UserEntrySorting entry = new UserEntrySorting();
                    if(nextEntry.NextEntryId_Id != null) entry = _entrySortingRepository.GetByEntryId(sortingId, (int)nextEntry.NextEntryId_Id);

                    foreach (ProductDto product in products)
                    {
                        if (entry.ShoppingListEntry_Id == product.Id)
                        {
                            sortedList.Add(product);
                            if(entry.NextEntryId_Id != null) nextEntry = _entrySortingRepository.GetByEntryId(sortingId, (int)entry.NextEntryId_Id);
                        }
                    }

                    if (entry.NextEntryId_Id == null) return sortedList;
                }
            }
             

            return sortedList;
        }

        public void SaveSorting(UserListSortingDto dto, IList<ProductDto> products)
        {
            //if exists -> update | else -> create
            if(_listSortingRepository.Get(dto.Id) != null)
            {
                UpdateSorting(dto, products);
            }
            else
            {
                CreateSorting(dto, products);
            }
        }

        public void SaveSorting(UserListSortingDto dto, IList<ProductDto> products, int deletedEntryId)
        {
            //if exists -> update | else -> create
            if (_listSortingRepository.Get(dto.Id) != null)
            {
                UpdateSorting(dto, products, deletedEntryId);
            }
            else
            {
                CreateSorting(dto, products);
            }
        }

        //TODO: create new sorting + UserEntrySortings
        public void CreateSorting(UserListSortingDto dto, IList<ProductDto> products)
        {
            int sortingId = _listSortingRepository.Create(ConvertDtoToDB(dto));

            for(int x = 0; x < products.Count(); x++)
            {
                Nullable<int> prevEntryId = null;
                if (x > 0) prevEntryId = products[x - 1].Id;
                Nullable<int> nextEntryId = null;
                if (x < products.Count()-1) nextEntryId = products[x + 1].Id;

                UserEntrySorting entrySorting = new UserEntrySorting
                {
                    UserListSorting_Id = sortingId,
                    ShoppingListEntry_Id = products[x].Id,
                    PrevEntryId_Id = prevEntryId,
                    NextEntryId_Id = nextEntryId                    
                };

                _entrySortingRepository.Create(entrySorting);
            }

            ShoppingListService shoppingListService = new ShoppingListService();
            ShoppingListDto shoppingList = new ShoppingListDto();
            shoppingList.Id = dto.ShoppingList_Id;
            shoppingList.ChosenSortingId = sortingId;
            shoppingListService.Update(shoppingList);
        }


        //TODO: update (sorting) -> UserEntrySortings
        public void UpdateSorting(UserListSortingDto dto, IList<ProductDto> products)
        {
            _listSortingRepository.Update(ConvertDtoToDB(dto));


            for (int x = 0; x < products.Count(); x++)
            {
                Nullable<int> prevEntryId;
                if (x == 0) prevEntryId = null;
                else prevEntryId = products[x - 1].Id;
                Nullable<int> nextEntryId;
                if(x == products.Count()-1) nextEntryId = null;
                else nextEntryId = products[x + 1].Id;

                _entrySortingRepository.Update(new UserEntrySorting
                {
                    UserListSorting_Id = dto.Id,
                    ShoppingListEntry_Id = products[x].Id,
                    PrevEntryId_Id = prevEntryId,
                    NextEntryId_Id = nextEntryId
                });
            }
        }

        public void UpdateSorting(UserListSortingDto dto, IList<ProductDto> products, int deletedEntryId)
        {
            var allEntrySortings = _entrySortingRepository.GetEntrySortings(dto.Id);

            foreach (UserEntrySorting userEntrySorting in allEntrySortings)
            {
                if (userEntrySorting.ShoppingListEntry_Id == deletedEntryId)
                {
                    if (userEntrySorting.PrevEntryId_Id != null)
                    {
                        var prevEntrySorting = _entrySortingRepository.GetByEntryId(dto.Id, (int)userEntrySorting.PrevEntryId_Id);
                        prevEntrySorting.NextEntryId_Id = userEntrySorting.NextEntryId_Id;
                        _entrySortingRepository.Update(prevEntrySorting);
                    }

                    if (userEntrySorting.NextEntryId_Id != null)
                    {
                        var nextEntrySorting = _entrySortingRepository.GetByEntryId(dto.Id, (int)userEntrySorting.NextEntryId_Id);
                        nextEntrySorting.PrevEntryId_Id = userEntrySorting.PrevEntryId_Id;
                        _entrySortingRepository.Update(nextEntrySorting);
                    }

                    _entrySortingRepository.Delete(userEntrySorting);
                }
            }
        }


        protected override UserListSortingDto ConvertDBToDto(UserListSorting entity)
        {
            return StaticDBToDto(entity);
        }

        protected override UserListSorting ConvertDtoToDB(UserListSortingDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static UserListSorting StaticDtoToDB(UserListSortingDto dto)
        {
            if (dto == null) return null;
            Nullable<int> shopId = null;
            if (dto.ShopId != 0) shopId = dto.ShopId;
            return new UserListSorting
            {
                Id = dto.Id,
                Name = dto.UserSortingName,
                Shop_Id = shopId,
                ShoppingList_Id = dto.ShoppingList_Id
            };
        }

        public static UserListSortingDto StaticDBToDto(UserListSorting entity)
        {
            if (entity == null) return null;
            return new UserListSortingDto
            {
                Id = entity.Id,
                UserSortingName = entity.Name,
                ShopId = (int)entity.Shop_Id,
                ShoppingList_Id = entity.ShoppingList_Id
            };
        }
    }
}
