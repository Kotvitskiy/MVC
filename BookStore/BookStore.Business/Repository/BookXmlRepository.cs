using Store.Business.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;

namespace Store.Business.Repository
{
    public class BookXmlRepository : BaseRepository<BookItem>
    {
        public BookXmlRepository()
        {
            FilePath = HttpContext.Current.Server.MapPath("~/App_Data/Books.xml");
        }

        public override void Save(IEnumerable<BookItem> items)
        {
            var doc = new XDocument();

            var root = new XElement("Books");

            doc.Add(root);

            foreach(var item in items)
            {
                root.Add(new XElement("book",
                    new XAttribute("name", item.Name),
                    new XAttribute("PublishingHouse", item.PublishingHouse)));
            }

            doc.Save(FilePath);
        }

        public override IEnumerable<BookItem> GetAll()
        {
            var results = new List<BookItem>();

            var doc = XDocument.Load(FilePath);

            foreach (XElement element in doc.Root.Elements())
            {
                var tmp = new BookItem();

                tmp.Name = element.Attribute("name").Value;

                tmp.PublishingHouse = element.Attribute("PublishingHouse").Value;

                results.Add(tmp);
            }

            return results.OrderByDescending(x => x.Name).ToList();
        }
    }
}
