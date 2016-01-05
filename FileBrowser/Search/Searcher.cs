using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Search;
using System.IO;
using Lucene.Net.QueryParsers;

namespace FileBrowser.Search
{
    public class Searcher
    {
        private IndexSearcher _indexSearcher;
        private string _indexDirectory;

        public Searcher(string indexDirectory)
        {
            _indexDirectory = indexDirectory;
        }

        public ScoreDoc[] Search(string keyword)
        {
            int num = 10;
            IndexReader reader = null;
            IndexSearcher searcher = null;
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            PerFieldAnalyzerWrapper wrapper = new PerFieldAnalyzerWrapper(analyzer);
            wrapper.AddAnalyzer("Name", analyzer);
            string[] fields = { "Name" };
            // try
            // {
                reader = IndexReader.Open(FSDirectory.Open(new DirectoryInfo(_indexDirectory)), true);
                searcher = new IndexSearcher(reader);
                QueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, fields, wrapper);
                Query query = parser.Parse(keyword);
                TopScoreDocCollector collector = TopScoreDocCollector.Create(num, true);

                searcher.Search(query, collector);
                var hits = collector.TopDocs().ScoreDocs;
                for (int i = 0; i < hits.Count(); i++)
                {
                    var hit = hits[i];
                    Document doc = searcher.Doc(hit.Doc);
                    Field fileNameField = doc.GetField("Name");
                    Field pathField = doc.GetField("Path");
                }
                return hits;
            // }
        }
    }
}
