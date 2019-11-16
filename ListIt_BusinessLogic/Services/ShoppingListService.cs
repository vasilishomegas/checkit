using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DomainModel;

namespace ListIt_BusinessLogic.Services
{
    class ShoppingListService : Service<ShoppingList>
    {
        public ICollection<ShoppingList> GetListsNewerThan(DateTime dateTime)
        {
            return _repository.GetAll().Where(shoppingList => shoppingList.Timestamp >= dateTime).ToList();
        }
    }
}
