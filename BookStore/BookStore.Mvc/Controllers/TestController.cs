using Store.Core.CustomControllerFactories;
using Store.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Mvc.Controllers
{
    public class TestController : Controller, ITestSwitcher
    {
        //
        // GET: /Test/
        public ActionResult Index()
        {
            return View(new BookItemViewModel() {Name="Test"});
        }
	}
}