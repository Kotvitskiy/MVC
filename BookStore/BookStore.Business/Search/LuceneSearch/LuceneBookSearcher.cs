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
using System.Web;
using System.Linq;

namespace Store.Business.Search.LuceneSearch
{
    public class LuceneBookSearcher : ISearchService<BookItem>
    {
        private Analyzer analyzer;

        private string indexPath;

        public LuceneBookSearcher()
        {
            this.indexPath = HttpContext.Current.Server.MapPath("~/App_Data/LuceneIndex/Book");
            InitialiseLucene();
        }

        private void InitialiseLucene()
        {
            analyzer = new StandardAnalyzer(global::Lucene.Net.Util.Version.LUCENE_30);
        }

        public void BuildIndex(IEnumerable<BookItem> items)
        {

            using(var luceneIndexDirectory = FSDirectory.Open(indexPath))
            {
                using (var writer = new IndexWriter(luceneIndexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    foreach (var obj in items)
                    {
                        Document doc = new Document();

                        doc.Add(new Field("Name",
                        obj.Name,
                        Field.Store.YES,
                        Field.Index.ANALYZED));
                        doc.Add(new Field("Publishing",
                        obj.PublishingHouse,
                        Field.Store.YES,
                        Field.Index.ANALYZED));

                        writer.AddDocument(doc);
                    }

                    writer.Optimize();
                    writer.Flush(false, true, false);
                }
            }
        }

        public IEnumerable<BookItem> Search(string searchTerm)
        {
            int hitsPerPage = 1000;

            using (var luceneIndexDirectory = FSDirectory.Open(indexPath))
            {
                using(var reader = IndexReader.Open(luceneIndexDirectory, true))
                {
                    using(var searcher = new IndexSearcher(reader))
                    {
                        var parser = new MultiFieldQueryParser(global::Lucene.Net.Util.Version.LUCENE_30, new string[] {"Name", "Publishing"}, analyzer);

                        var collector = TopScoreDocCollector.Create(hitsPerPage, true);

                        var query = parser.Parse(searchTerm);

                        searcher.Search(query, collector);

                        var hits = collector.TopDocs().ScoreDocs;

                        var results = new List<BookItem>();

                        for (int i = 0; i < hits.Length; i++)
                        {
                            var tmp = new BookItem();

                            int docId = hits[i].Doc;

                            var doc = searcher.Doc(docId);

                            tmp.Name = doc.Get("Name");

                            tmp.PublishingHouse = doc.Get("Publishing");

                            results.Add(tmp);
                        }

                        return results.OrderByDescending(x => x.Name).ToList();
                    }
                }
            }
        }
    }
}
