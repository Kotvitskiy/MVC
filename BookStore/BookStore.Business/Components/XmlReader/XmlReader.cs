using BookStore.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookStore.Business.Components.XmlReader
{
    public class XmlReader
    {
        string XmlPath;

        public XmlReader(string path)
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
    }
}
