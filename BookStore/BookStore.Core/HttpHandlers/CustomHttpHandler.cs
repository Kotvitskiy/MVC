using Store.Core.RouteHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Store.Core.HttpHandlers
{
    public class CustomHttpHandler : IHttpHandler
    {
        public RequestContext RequestContext = null;

        public CustomHttpHandler(RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.RequestType == "POST")
                {
                    context.Response.RedirectToRoute(GetRouteName(), context.Request.Form);
                }
                else
                {
                    context.Response.RedirectToRoute(GetRouteName());
                }
                context.Response.Status = "307 Temporary Redirect";
            }
            catch
            {
                context.Response.Expires = 0;
                context.Response.Buffer = true;
                context.Response.Status = "404 Not Found";
            }
        }

        private string GetRouteName()
        {
            return RequestContext.RouteData.Values["routeName"].ToString();
        }
    }
}
