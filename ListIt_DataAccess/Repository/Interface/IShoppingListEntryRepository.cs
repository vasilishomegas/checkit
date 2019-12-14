using System.Collections.Generic;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository.Interface
{
    public interface IShoppingListEntryRepository : IRepository<ShoppingListEntry>
    {
        new IEnumerable<ShoppingListEntry> GetAll();
        IEnumerable<ShoppingListEntry> GetEntriesByListId(int listId);
    }
}
