using System.Collections.Generic;
using System.Linq;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class UserEntrySortingRepository : Repository<UserEntrySorting>
    {
        public IEnumerable<UserEntrySorting> GetEntrySortings(int id)
        {
            using (var context = new ListItContext())
            {
                return context.UserEntrySortings
                    .Where(x => x.UserListSorting_Id == id)
                    .ToList();
            }
        }

        public UserEntrySorting GetByEntryId(int sortingId, int id)
        {
            using (var context = new ListItContext())
            {
                var obj = context.UserEntrySortings
                    .Where(x => x.UserListSorting_Id == sortingId)
                    .SingleOrDefault(x => x.ShoppingListEntry_Id == id);
                if (obj == null) return null;
                return obj;
            }
        }



        public UserEntrySorting GetFirstEntry(int sortingId)
        {
            using (var context = new ListItContext())
            {
                return context.UserEntrySortings
                    .Where(x => x.UserListSorting_Id == sortingId)
                    .SingleOrDefault(x => x.PrevEntryId_Id == null);
            }
        }

        public void Delete(UserEntrySorting sorting)
        {
            using (var context = new ListItContext())
            {
                var entity = context.Set<UserEntrySorting>()
                    .Where(x => x.ShoppingListEntry_Id == sorting.ShoppingListEntry_Id)
                    .FirstOrDefault(x => x.UserListSorting_Id == sorting.UserListSorting_Id);
                try
                {
                    context.Set<UserEntrySorting>().Remove(entity);
                }
                catch (System.ArgumentNullException e)
                {
                    throw new KeyNotFoundException("No entries were affected; the row does not exist." + e.Message);
                }
                context.SaveChanges();
            }
        }
    }
}