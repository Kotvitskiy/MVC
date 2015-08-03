using Store.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Store.Business.Components.XmlServices
{
    public class XMLService
    {
        string XmlPath;

        public XMLService(string path)
        {
            XmlPath = path;
        }

        public IEnumerable<BookItem> MapXmlToObject()
        {
            var results = new List<BookItem>();

            var doc = XDocument.Load(XmlPath);

            foreach(XElement element in doc.Root.Elements())
            {
                var tmp = new BookItem();

                tmp.Name = element.Attribute("name").Value;

                tmp.PublishingHouse = element.Attribute("PublishingHouse").Value;

                results.Add(tmp);
            }

            return results.OrderByDescending(x => x.Name).ToList();
        }

        public void MapObjectToXml()
        {
            var doc = new XDocument();

            var root = new XElement("Books");

            doc.Add(root);

            for (int i = 0; i < 10000; i++)
            {
                root.Add(new XElement("book",
                    new XAttribute("name", "Book" + i),
                    new XAttribute("PublishingHouse", "Publishing" + i)));
            }

            doc.Save(XmlPath);
        }
    }
}
