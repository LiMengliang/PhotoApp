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
using FileBrowser.ViewModel;

namespace FileBrowser.Search
{
    public enum SearchPurpose 
    {
        Name,
        Color
    }

    public enum SearchScope
    {
        AllDisk,
        CurrentDirectory
    }

    public class Searcher
    {
        // private IndexSearcher _indexSearcher;
        private string _indexDirectory;
        private IList<DirectoryInfo> _indexDirectories;

        public SearchPurpose SearchPurpose { get; set; }

        public SearchScope SerchScope { get; set; }

        public Searcher()
        {             
        }

        public Searcher(string indexDirectory)
        {
            _indexDirectory = indexDirectory;
            var indexRootDirectory = new DirectoryInfo(_indexDirectory);
            _indexDirectories = indexRootDirectory.GetDirectories();
            // IndexReader reader = IndexReader.Open(FSDirectory.Open(new DirectoryInfo(_indexDirectory)), true);
            // _indexSearcher = new IndexSearcher(reader);
        }

        public IList<SearchItemInfo> Search(string keyword)
        {
            int num = 1000;
            var hits = new List<SearchItemInfo>();
            var fileName = SearchPurpose == SearchPurpose.Name ? "Name" : "Color";
            var term = new Term(fileName, keyword);
            foreach(var indexDirectory in _indexDirectories)
            {
                IndexReader reader = IndexReader.Open(FSDirectory.Open(indexDirectory), true);
                var fuzyQuery = new TermQuery(term);
                TopScoreDocCollector collector = TopScoreDocCollector.Create(num, true);
                var indexSearcher = new IndexSearcher(reader);
                indexSearcher.Search(fuzyQuery, collector);
                foreach(var scoreDoc in collector.TopDocs().ScoreDocs)
                {
                    Field fileNameField = indexSearcher.Doc(scoreDoc.Doc).GetField("Name");
                    Field pathField = indexSearcher.Doc(scoreDoc.Doc).GetField("Path");
                    Field typeField = indexSearcher.Doc(scoreDoc.Doc).GetField("Type");
                    var fileItemType = FileItemType.File;
                    switch (typeField.StringValue)
                    { 
                        case "File":
                            fileItemType = FileItemType.File;
                            break;
                        case "Directory":
                            fileItemType = FileItemType.Directory;
                            break;
                        case "Driver":
                            fileItemType = FileItemType.Drive;
                            break;

                    }
                    hits.Add(new SearchItemInfo(pathField.StringValue, fileNameField.StringValue, fileItemType));
                }
            }
            return hits;
        }

        // public ScoreDoc[] Search(string keyword)
        // {
        //     int num = 1000;
        //     IndexReader reader = null;
        //     var term = new Term("Name", keyword);
        //     var fuzyQuery = new FuzzyQuery(term);
        //     var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
        //     PerFieldAnalyzerWrapper wrapper = new PerFieldAnalyzerWrapper(analyzer);
        //     wrapper.AddAnalyzer("Name", analyzer);
        //     string[] fields = { "Name" };
        //     // try
        //     // {
        //         // reader = IndexReader.Open(FSDirectory.Open(new DirectoryInfo(_indexDirectory)), true);
        //         // searcher = new IndexSearcher(reader);
        //         QueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, fields, wrapper);
        //         var query = parser.Parse(keyword);
        //         
        //         TopScoreDocCollector collector = TopScoreDocCollector.Create(num, true);
        //     
        //         _indexSearcher.Search(query, collector);
        //         var hits = collector.TopDocs().ScoreDocs;
        //         
        //         return hits;
        //     // }
        // 
        //     // int num = 100;
        //     // IndexReader reader = null;
        //     // var term = new Term("Name", keyword);
        //     // var fuzyQuery = new FuzzyQuery(term);
        //     // TopScoreDocCollector collector = TopScoreDocCollector.Create(num, true);
        //     // _indexSearcher.Search(fuzyQuery, collector);
        //     // var hits = collector.TopDocs().ScoreDocs;
        //     // 
        //     // return hits;
        //     // }
        // }

        // public Document GetDocument(int docIndex)
        // {
        //     if (_indexSearcher != null)
        //     {
        //         return _indexSearcher.Doc(docIndex);
        //     }
        //     return null;
        // }
    }
}
