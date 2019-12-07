using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Repository;

namespace ListIt_DataAccess.Repository
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        public IEnumerable<Language> GetAllNames()
        {
            using (var context = new ListItContext())
            {
                return context.Set<Language>().ToList();
            }
        }

        public Language GetByCode(string code)
        {
            using (var context = new ListItContext())
            {
                return context.Languages
                        .SingleOrDefault(x => x.Code == code);
            }
        }
    }
}