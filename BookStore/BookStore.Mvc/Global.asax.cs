using Store.Core.CustomViewEngineLocation;
using Store.Mvc.Components.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Store.Core.CustomControllerFactories;

namespace Store.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperWebConfiguration.Configure();
        }
    }
}
