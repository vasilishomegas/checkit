using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;

namespace ListIt_DataAccess.Repository.Helpers
{
    public abstract class ContextManager
    {
        public static void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var builder = new StringBuilder();
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
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
            {
                throw new KeyNotFoundException(e.Message, e.InnerException);
            }
        }
    }
}