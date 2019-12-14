using System.Collections.Generic;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository.Interface
{
    public interface IUnitTypeRepository : IRepository<UnitType>
    {
        IList<TranslationOfUnitType> GetUnitTypesByLanguage(int langId);
    }
}
