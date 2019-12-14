using System.Collections.Generic;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetUserCategoryIds(int userid);
        IEnumerable<Category> GetDefaultCategoryIds();
        TranslationOfCategory Get(int id, int langId);
    }
}
