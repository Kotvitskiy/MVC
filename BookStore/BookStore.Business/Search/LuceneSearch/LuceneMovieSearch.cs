using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Store.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Store.Business.Extensions;

namespace Store.Business.Search.LuceneSearch
{
    public class LuceneMovieSearcher : ISearchService<MovieItem>
    {
        private Analyzer analyzer;

        private string indexPath;

        public LuceneMovieSearcher()
        {
            this.indexPath = HttpContext.Current.Server.MapPath("~/App_Data/LuceneIndex/Movie");
            InitialiseLucene();
        }

        private void InitialiseLucene()
        {
            analyzer = new StandardAnalyzer(global::Lucene.Net.Util.Version.LUCENE_30);
        }

        public void BuildIndex(IEnumerable<MovieItem> items)
        {

            using(var luceneIndexDirectory = FSDirectory.Open(indexPath))
            {
                using (var writer = new IndexWriter(luceneIndexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    foreach (var item in items)
                    {
                        Document doc = new Document();

                        doc.Add(new Field("Name",
                        item.Name,
                        Field.Store.YES,
                        Field.Index.ANALYZED));

                        doc.Add(new Field("Duration",
                        item.Duration.ToString(),
                        Field.Store.YES,
                        Field.Index.ANALYZED));

                        doc.Add(new Field("DisplayResolution",
                            item.DisplayResolution.ToString(),
                            Field.Store.YES,
                            Field.Index.NO));

                        writer.AddDocument(doc);
                    }

                    writer.Optimize();
                    writer.Flush(false, true, false);
                }
            }
        }

        public IEnumerable<MovieItem> Search(string searchTerm)
        {
            int hitsPerPage = 1000;

            using (var luceneIndexDirectory = FSDirectory.Open(indexPath))
            {
                using(var reader = IndexReader.Open(luceneIndexDirectory, true))
                {
                    using(var searcher = new IndexSearcher(reader))
                    {
                        var parser = new QueryParser(global::Lucene.Net.Util.Version.LUCENE_30, "Name", analyzer);

                        var collector = TopScoreDocCollector.Create(hitsPerPage, true);

                        var query = parser.Parse(searchTerm);

                        searcher.Search(query, collector);

                        var hits = collector.TopDocs().ScoreDocs;

                        var results = new List<MovieItem>();

                        for (int i = 0; i < hits.Length; i++)
                        {
                            var tmp = new MovieItem();

                            int docId = hits[i].Doc;

                            var doc = searcher.Doc(docId);

                            tmp.Name = doc.Get("Name");

                            tmp.Duration = doc.Get("Duration").ToTimeSpan();

                            tmp.DisplayResolution = doc.Get("DisplayResolution").ToDisplayResolution();

                            results.Add(tmp);
                        }

                        return results.OrderByDescending(x => x.Name).ToList();
                    }
                }
            }
        }
    }
}
