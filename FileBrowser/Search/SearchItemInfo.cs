using FileBrowser.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Search
{
    public class SearchItemInfo
    {
        private Lucene.Net.Documents.Field pathField;
        private Lucene.Net.Documents.Field fileNameField;

        public string Path { get; set; }
        public FileItemType FileItemType { get; set; }
        public string Name { get; set; }
        public SearchItemInfo(string path, string name, FileItemType fileItemType)
        {
            Path = path;
            Name = name;
            FileItemType = fileItemType;
        }

    }
}
