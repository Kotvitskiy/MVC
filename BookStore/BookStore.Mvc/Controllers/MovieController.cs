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
using System.Threading;
using System.Threading.Tasks;

namespace Store.Mvc.Controllers
{
    public class MovieController : AsyncController
    {
        ISearchService<MovieItem> searcher;

        IRepository<MovieItem> repository;

        public MovieController(ISearchService<MovieItem> searcher, IRepository<MovieItem> repository)
        {
            this.searcher = searcher;

            this.repository = repository;
        }

        public async void InitLuceneAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            var movies = await Task.Factory.StartNew(() => repository.GetAll());
            //var movies = repository.GetAll();
            searcher.BuildIndex(movies);
            AsyncManager.OutstandingOperations.Decrement();
        }

        public string InitLuceneCompleted()
        {
            return "Indexing Done";
        }

        public ActionResult List()
        {
            var modelList = new List<MovieItemViewModel>();
            modelList.Add(new MovieItemViewModel { Name = "33", Duration = "1:33".ToTimeSpan(), DisplayResolution = "330x440".ToDisplayResolution() });
            return View(modelList);
        }

        public ActionResult CreateXml()
        {

            var list = new List<MovieItem>();

            for (int i = 0; i < 100000; i++)
            {
                list.Add(new MovieItem { Name = "Movie" + i, Duration = ("1:33").ToTimeSpan(),
                    DisplayResolution = ("330"+i+"x"+440+i).ToDisplayResolution()});
            }

            repository.Save(list);

            return RedirectToRoute("Store");
        }

        [HttpPost]
        public void SearchAsync(string searchString)
        {
            AsyncManager.OutstandingOperations.Increment();
            var result = searcher.Search(searchString);
            var viewResult = Mapper.Map<IList<MovieItem>, IList<MovieItemViewModel>>((List<MovieItem>)result);
            AsyncManager.Parameters["headlines"] = viewResult;
            AsyncManager.OutstandingOperations.Decrement();
        }

        [HttpPost]
        public ActionResult SearchCompleted(IList<MovieItemViewModel> viewResult)
        {
            return Json(ViewHelper.RenderRazorViewToString(this.ControllerContext, "~/Views/Movie/MoviePartial.cshtml", viewResult));
        }

        //[HttpPost]
        //public ActionResult Search(string searchString)
        //{
        //    var result = searcher.Search(searchString);

        //    var viewResult = Mapper.Map<IList<MovieItem>, IList<MovieItemViewModel>>((List<MovieItem>)result);

        //    return Json(ViewHelper.RenderRazorViewToString(this.ControllerContext, "~/Views/Movie/MoviePartial.cshtml", viewResult));
        //}
	}
}