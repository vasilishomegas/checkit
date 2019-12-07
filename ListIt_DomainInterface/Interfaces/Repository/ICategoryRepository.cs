﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_DataAccessModel;

namespace ListIt_DomainInterface.Interfaces.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetUserCategoryIds(int userid);
        IEnumerable<Category> GetDefaultCategoryIds();
        TranslationOfCategory Get(int id, int langId);
    }
}
