using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using System.Text;

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

        public UserEntrySorting GetByPrevEntryId(int sortingId, int id)
        {
            using (var context = new ListItContext())
            {
                return context.UserEntrySortings
                    .Where(x => x.UserListSorting_Id == sortingId)
                    .SingleOrDefault(x => x.PrevEntryId_Id != null && x.PrevEntryId_Id == id);
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
    }
}