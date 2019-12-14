using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccess.Repository.Interface;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
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
                return context.TranslationOfCategories
                    .Where(x => x.Language_Id == langId)
                    .SingleOrDefault(x => x.Category_Id == id);
            }
        }
    }
}