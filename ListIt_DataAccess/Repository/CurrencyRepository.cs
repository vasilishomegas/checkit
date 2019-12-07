using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Repository;

namespace ListIt_DataAccess.Repository
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {

        //TODO: get names from Translation table
    }
}