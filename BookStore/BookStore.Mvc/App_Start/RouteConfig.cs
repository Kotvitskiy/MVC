using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BookStore.Core.RouteHandlers;

namespace BookStore.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add(new Route("[Route:{routeName}]", new CustomRouteHandler()));

            routes.MapRoute(
                name: "Test",
                url: "Test",
                defaults: new { controller = "Test", action = "Index"});

            routes.MapRoute(
                name: "PageNotFound",
                url: "PageNotFound",
                defaults: new { controller = "Error", action = "PageNotFound" });

            routes.MapRoute(
                name: "Home",
                url: "Home",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                name: "Data",
                url: "Data",
                defaults: new { controller = "Home", action = "Data"}
            );

            routes.MapRoute(
                name: "Test1",
                url: "Test1",
                defaults: new { controller = "NewTest", action = "Index1" }
            );

            routes.MapRoute(
               name: "Store",
               url: "Store",
               defaults: new { controller = "Store", action = "List" }
           );

            routes.MapRoute(
               name: "CreateXml",
               url: "CreateXml",
               defaults: new { controller = "Store", action = "CreateXml" }
           );

            routes.MapRoute(
               name: "InitLucene",
               url: "InitLucene",
               defaults: new { controller = "Store", action = "InitLucene" }
           );

            routes.MapRoute(
               name: "Search",
               url: "Search",
               defaults: new { controller = "Store", action = "Search" }
           );
        }
    }
}
