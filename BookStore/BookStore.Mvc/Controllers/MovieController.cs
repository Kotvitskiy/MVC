using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Business.Repository;
using Store.Business.Search;
using Store.Business.Entities;
using Store.Mvc.Models;
using Store.Business.Extensions;
using AutoMapper;
using Store.Mvc.Helper;

namespace Store.Mvc.Controllers
{
    public class MovieController : Controller
    {
        ISearchService<MovieItem> searcher;

        IRepository<MovieItem> repository;

        public MovieController(ISearchService<MovieItem> searcher, IRepository<MovieItem> repository)
        {
            this.searcher = searcher;

            this.repository = repository;
        }

        public ActionResult List()
        {
            var modelList = new List<MovieItemViewModel>();
            modelList.Add(new MovieItemViewModel { Name = "33", Duration = "1.33".ToTimeSpan(), DisplayResolution = "330x440".ToDisplayResolution() });
            return View(modelList);
        }

        public ActionResult CreateXml()
        {

            var list = new List<MovieItem>();

            for (int i = 0; i < 1000; i++)
            {
                list.Add(new MovieItem { Name = "Movie" + i, Duration = ("1.33"+i).ToTimeSpan(),
                    DisplayResolution = ("330"+i+"x"+440+i).ToDisplayResolution()});
            }

            repository.Save(list);

            return RedirectToRoute("Store");
        }

        public string InitLucene()
        {
            var movies = repository.GetAll();

            searcher.BuildIndex(movies);

            return "Indexing Done";
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            var result = searcher.Search(searchString);

            var viewResult = Mapper.Map<IList<BookItem>, IList<BookItemViewModel>>((List<BookItem>)result);

            return Json(ViewHelper.RenderRazorViewToString(this.ControllerContext, "~/Views/Movie/MoviePartial.cshtml", viewResult));
        }
	}
}