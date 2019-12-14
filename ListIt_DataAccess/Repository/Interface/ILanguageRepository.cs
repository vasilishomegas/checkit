﻿using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository.Interface
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Language GetByCode(string code);
    }
}
