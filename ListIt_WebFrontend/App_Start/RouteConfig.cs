using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ListIt_WebFrontend
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Launch", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "User",
                url: "user/{id}/{action}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Store",
                url: "store/{action}/{id}",
                defaults: new { controller = "Store", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "List",
                url: "user/lists/{action}/{id}",
                defaults: new { controller = "List", action = "Index", id = UrlParameter.Optional, listId = UrlParameter.Optional, templateId = UrlParameter.Optional, sortingId = UrlParameter.Optional }
            );
        }
    }
}
