using System;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using System.Text;

namespace ListIt_DataAccess.Repository
{
    public class UserListSortingRepository : Repository<UserListSorting>
    {
        public new int Create(UserListSorting sorting)
        {
            using (var context = new ListItContext())
            {
                var entry = context.Set<UserListSorting>().Add(sorting);

                try
                {
                    context.SaveChanges();
                    return entry.Id;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        builder.Append("Entity of type " + eve.Entry.Entity.GetType().Name
                                                         + " in state " + eve.Entry.State + " has the following" +
                                                         " validation errors:");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            builder.Append("Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage);
                        }
                    }

                    throw new Exception(builder.ToString());
                }
            }
        }

        public override UserListSorting Get(int id)
        {
            using (var context = new ListItContext())
            {
                var sorting =  context.Set<UserListSorting>().Find(id);
                if (sorting == null) return null;
                return sorting;
            }
        }
    }
}