using Store.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using Store.Business.Extensions;

namespace Store.Business.Repository
{
    public class MovieXmlRepository : BaseRepository<MovieItem>
    {
        public MovieXmlRepository()
        {
            FilePath = HttpContext.Current.Server.MapPath("~/App_Data/Movies.xml");
        }

        public override void Save(IEnumerable<MovieItem> items)
        {
            var doc = new XDocument();

            var root = new XElement("Movies");

            doc.Add(root);

            foreach(var item in items)
            {
                root.Add(new XElement("movie",
                    new XAttribute("Name", item.Name),
                    new XAttribute("Duration", item.Duration.ToString()),
                    new XAttribute("DisplayResolution", item.DisplayResolution.ToString())));
            }

            doc.Save(FilePath);
        }

        public override IEnumerable<MovieItem> GetAll()
        {
            var results = new List<MovieItem>();

            var doc = XDocument.Load(FilePath);

            foreach (XElement element in doc.Root.Elements())
            {
                var tmp = new MovieItem();

                tmp.Name = element.Attribute("Name").Value;

                tmp.Duration = element.Attribute("Duration").Value.ToTimeSpan();

                tmp.DisplayResolution = element.Attribute("DisplayResolution").Value.ToDisplayResolution();

                results.Add(tmp);
            }

            return results.OrderByDescending(x => x.Name).ToList();
        }
    }
}
