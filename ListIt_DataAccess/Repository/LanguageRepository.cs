﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class LanguageRepository : Repository<Language>
    {
        public IEnumerable<Language> GetAllNames()
        {
            using (var context = new ListItContext())
            {
                // Invoke .ToList() method to execute IQueryable. If you returned IQueryable, the ListItContext
                // would already be disposed by "using" statement, which automatically disposes disposable objects.
                return context.Set<Language>().ToList();
            }
        }
    }
}