using ListIt_DataAccessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListIt_DomainInterface.Interfaces.Repository
{
    public interface IShoppingListEntryRepository : IRepository<ShoppingListEntry>
    {
        new IEnumerable<ShoppingListEntry> GetAll();
        IEnumerable<ShoppingListEntry> GetEntriesByListId(int listId);
        Product Get(int id);
        void CreateProduct(Product product);
        void CreateUserProduct(UserProduct product);

    }
}
