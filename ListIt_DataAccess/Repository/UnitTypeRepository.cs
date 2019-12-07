using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Repository;

namespace ListIt_DataAccess.Repository
{
    public class UnitTypeRepository : Repository<UnitType>, IUnitTypeRepository
    {
        //TODO: get names from translation table
        public IList<TranslationOfUnitType> GetUnitTypesByLanguage(int langId)
        {
            using (var context = new ListItContext())
            {
                return context.TranslationOfUnitTypes
                    .Where(x => x.Language_Id == langId)
                    .ToList();
            }
        }


    }
}