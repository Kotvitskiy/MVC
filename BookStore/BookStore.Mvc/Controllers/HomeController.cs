using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Mvc.Controllers
{
    public class HomeController : Controller
    {
        // 
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Data(string name, string age)
        {
            return Json(name + " " + age);
        }
	}
}