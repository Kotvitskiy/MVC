using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lucene;
using System.Xml.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Store.Business.Components.Lucene;
using Store.Business.Components.XmlServices;
using Store.Mvc.Models;
using System.IO;
using AutoMapper;
using Store.Business.Entities;
using Store.Mvc.Helper;
using Store.Business.Repository;
using Store.Business.Search;

namespace Store.Mvc.Controllers
{
    public class BookController : Controller
    {
        IRepository<BookItem> repository = null;

        ISearchService<BookItem> searcher = null;

        public BookController(IRepository<BookItem> repository, ISearchService<BookItem> searcher)
        {
            this.repository = repository;

            this.searcher = searcher;
        }


        public ActionResult List()
        {
            var modelList = new List<BookItemViewModel>();
            modelList.Add(new BookItemViewModel { Name = "Мастер и Маргарита", PublishingHouse = "Михаил Булгаков" });
            modelList.Add(new BookItemViewModel { Name = "Мастер и Маргарита", PublishingHouse = "Михаил Булгаков" });
            modelList.Add(new BookItemViewModel { Name = "Мастер и Маргарита", PublishingHouse = "Михаил Булгаков" });
            modelList.Add(new BookItemViewModel { Name = "Мастер и Маргарита", PublishingHouse = "Михаил Булгаков" });
            modelList.Add(new BookItemViewModel { Name = "Мастер и Маргарита", PublishingHouse = "Михаил Булгаков" });
            return View(modelList);
        }

        public ActionResult CreateXml()
        {
            repository.Save(new List<BookItem> {new BookItem {Name = "first", PublishingHouse= "FirstP"}});

            return RedirectToRoute("Store");
        }

        public string InitLucene()
        { 
            var books = repository.GetAll();

            searcher.BuildIndex(books);

            return "Indexing Done";
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            var result = searcher.Search(searchString);

            var viewResult = Mapper.Map<IList<BookItem>, IList<BookItemViewModel>>((List<BookItem>)result);

            return Json(ViewHelper.RenderRazorViewToString(this.ControllerContext, "~/Views/Book/BookPartial.cshtml", viewResult));
        }

        [HttpPost]
        public ActionResult AddBook(string name, string publishing)
        {
            var item = new BookItem { Name = name, PublishingHouse = publishing };
            return Json(ViewHelper.RenderRazorViewToString(this.ControllerContext, "~/Views/Book/BookPartial.cshtml", item));
        }
	}
}