using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Mvc.Models;

namespace BookStore.Mvc.Controllers
{
    public class NewTestController : Controller
    {
        //
        // GET: /NewTest/
        public ActionResult Index()
        {
            return View("~/Views/Test/Index1.cshtml", new BookViewModel());
        }
	}
}