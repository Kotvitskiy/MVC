using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Mvc.Models;
using Store.Core.CustomControllerFactories;

namespace Store.Mvc.Controllers
{
    public class NewTestController : Controller, ITestSwitcher
    {
        public ActionResult Index()
        {
            return View(new BookItemViewModel() { Name = "NewTest" });
        }
	}
}