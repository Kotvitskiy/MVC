using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Store.Core.HttpHandlers;

namespace Store.Core.RouteHandlers
{
    public class CustomRouteHandler : IRouteHandler
    {
        public CustomRouteHandler()
        {

        }

        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new CustomHttpHandler(requestContext);
        }
    }
}
