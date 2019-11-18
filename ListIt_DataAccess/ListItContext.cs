using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListIt_DataAccess
{
    public class ListItContext : dmaj0918_1074524Entities
    {
        public ListItContext() : base()
        {

            // SERIALIZATION ISSUE WITHOUT 2 LINES UNDERNEATH
            // https://stackoverflow.com/questions/23098191/failed-to-serialize-the-response-in-web-api-with-json
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}