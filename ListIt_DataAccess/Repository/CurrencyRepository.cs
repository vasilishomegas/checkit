using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_DataAccess.Repository.Generics;
using ListIt_DataAccess.Repository.Interface;
using ListIt_DataAccessModel;

namespace ListIt_DataAccess.Repository
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {

        //TODO: get names from Translation table
    }
}