using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Business.Entities;
using Lucene.Net.Documents;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.Standard;

namespace Store.Business.Components.Lucene
{
    public class LuceneService
    {
        private Analyzer analyzer;

        private Directory luceneIndexDirectory;

        private IndexWriter writer;

        private string indexPath;

        private IndexReader reader;

        public LuceneService()
        {
            this.indexPath = HttpContext.Current.Server.MapPath("~/App_Data/LuceneIndex");
            InitialiseLucene();
        }

        private void InitialiseLucene()
        {    
            luceneIndexDirectory = FSDirectory.Open(indexPath);
            analyzer = new StandardAnalyzer(global::Lucene.Net.Util.Version.LUCENE_30);
            try
            {
                writer = new IndexWriter(luceneIndexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            }
            catch (LockObtainFailedException)
            {
                writer.Dispose();
                writer = new IndexWriter(luceneIndexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            }
        }

        public void BuildIndex(IEnumerable<BookItem> items)
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
            writer.Dispose();
            luceneIndexDirectory.Dispose();
        }

        public IEnumerable<BookItem> Search(string searchTerm)
        {
            int hitsPerPage = 10;

            reader = IndexReader.Open(luceneIndexDirectory, true);

            var searcher = new IndexSearcher(reader);
            var parser = new QueryParser(global::Lucene.Net.Util.Version.LUCENE_30, "Name", analyzer);

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

            writer.Dispose();

            return results.OrderByDescending(x => x.Name).ToList();
        }
    }
}
