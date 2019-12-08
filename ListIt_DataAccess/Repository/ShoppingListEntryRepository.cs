using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class ShoppingListEntryRepository : Repository<ShoppingListEntry>
    {
        public override IEnumerable<ShoppingListEntry> GetAll()
        {
            using (var context = new ListItContext())
            {
                return context.ShoppingListEntries
                    .Include(x => x.Product)
                    .ToList();
            }
        }

        public IEnumerable<ShoppingListEntry> GetEntriesByListId(int listId)
        {
            using (var context = new ListItContext())
            {
                return context.ShoppingListEntries
                    .Where(x => x.ShoppingList_Id == listId)
                    .Include(x => x.Product)
                    .ToList();
            }
        }

        public ShoppingListEntry GetByProductAndListId(int productId, int listId)
        {
            using (var context = new ListItContext())
            {
                return context.ShoppingListEntries
                    .Where(x => x.ShoppingList_Id == listId)    //if reusbale or default: can appear in multiple lists
                    .SingleOrDefault(x => x.Product_Id == productId);
            }
        }

        
    }
}