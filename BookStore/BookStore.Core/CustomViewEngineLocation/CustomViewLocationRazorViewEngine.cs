using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookStore.Core.CustomViewEngineLocation
{
    class CustomViewLocationRazorViewEngine : RazorViewEngine
    {
        public CustomViewLocationRazorViewEngine()
        {
            ViewLocationFormats = new[] 
            {
                "~/Views/Test/{0}.cshtml", "~/RazorViews/{1}/{0}.vbhtml",
                "~/RazorViews/Test/{0}.cshtml", "~/RazorViews/Common/{0}.vbhtml"
            };
        }
    }
}
