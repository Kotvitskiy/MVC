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
using BookStore.Business.Components.Lucene;
using BookStore.Business.Components.XmlReader;
using BookStore.Mvc.Models;
using System.IO;
using AutoMapper;
using BookStore.Business.Entities;
using BookStore.Mvc.Helper;

namespace BookStore.Mvc.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/
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
            var doc = new XDocument();

            var root = new XElement("Books");

            doc.Add(root);

            for (int i = 0; i < 10000; i++)
            {
                root.Add(new XElement("book", 
                    new XAttribute("name", "Book"+i),
                    new XAttribute("PublishingHouse", "Publishing" +i)));
            }

            doc.Save(Server.MapPath("~/App_Data/Books.xml"));

            return RedirectToRoute("Store");
        }

        public string InitLucene()
        {
            XmlReader reader = new XmlReader(Server.MapPath("~/App_Data/Books.xml"));

            var books = reader.MapXmlToObject();
            
            LuceneService service = new LuceneService(Server.MapPath("~/App_Data/LuceneIndex"));

            service.BuildIndex(books);

            return "Indexing Done";
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            LuceneService service = new LuceneService(Server.MapPath("~/App_Data/LuceneIndex"));

            var result = service.Search(searchString);



            var viewResult = Mapper.Map<IList<BookItem>, IList<BookItemViewModel>>((List<BookItem>)result);

            return Json(ViewHelper.RenderRazorViewToString(this.ControllerContext, "~/Views/Store/BookPartial.cshtml", viewResult));
        }

        [HttpPost]
        public ActionResult AddBook(string name, string publishing)
        {
            var item = new BookItem { Name = name, PublishingHouse = publishing };
            return Json(ViewHelper.RenderRazorViewToString(this.ControllerContext, "~/Views/Store/BookPartial.cshtml", item));
        }
	}
}