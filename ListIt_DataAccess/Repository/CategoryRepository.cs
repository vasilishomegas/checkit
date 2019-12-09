using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using System.Text;

namespace ListIt_DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>
    {
        //get category names of translation table
        //Get all where user id == null && where user id matches session/passed userid

      

        public IEnumerable<Category> GetUserCategoryIds(int userid)
        {
            using (var context = new ListItContext())
            {
                return context.Categories
                    .Where(x => x.User_Id == userid)
                    .ToList();
            }
        }

        public IEnumerable<Category> GetDefaultCategoryIds()
        {
            using (var context = new ListItContext())
            {
                return context.Categories
                    .Where(x => x.User_Id == null)
                    .ToList();                    
            }
        }

        public TranslationOfCategory Get(int id, int langId)
        {
            using (var context = new ListItContext())
            {
                var translation = context.TranslationOfCategories
                    .Where(x => x.Language_Id == langId)
                    .SingleOrDefault(x => x.Category_Id == id);

                if(translation == null) translation = context.TranslationOfCategories
                    .FirstOrDefault(x => x.Category_Id == id);

                return translation;
            }
        }

        public new int Create(Category entity)
        {
            using (var context = new ListItContext())
            {
                var result = context.Set<Category>().Add(entity);
                try
                {
                    context.SaveChanges();
                    return result.Id;
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

        public void Create(TranslationOfCategory entity)
        {
            using (var context = new ListItContext())
            {
                var result = context.Set<TranslationOfCategory>().Add(entity);
                try
                {
                    context.SaveChanges();
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
    }
}