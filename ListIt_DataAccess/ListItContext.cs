using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_DataAccess
{
    public class ListItContext : dmaj0918_1074524Entity
    {
        public ListItContext() : base()
        {
            // https://stackoverflow.com/questions/14033193/entity-framework-provider-type-could-not-be-loaded
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;


            // SERIALIZATION ISSUE WITHOUT 2 LINES UNDERNEATH
            // https://stackoverflow.com/questions/23098191/failed-to-serialize-the-response-in-web-api-with-json
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}