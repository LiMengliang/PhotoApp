using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FileBrowser.Search
{
    public class IndexCreator
    {
        private Dispatcher _uiDispatcher;
        private BackgroundWorker _createIndexWorker;

        public IndexCreator(Dispatcher uiDispatcher)
        {
            _uiDispatcher = uiDispatcher;
        }

        public void CreateSearchIndex()
        {
            if (_createIndexWorker == null)
            {
                _createIndexWorker = new BackgroundWorker();
            }
            _createIndexWorker.WorkerSupportsCancellation = true;
            _createIndexWorker.DoWork += new DoWorkEventHandler(DoCreateSearchIndex);
            _createIndexWorker.RunWorkerAsync();
        }

        private void DoCreateSearchIndex(object sender, DoWorkEventArgs args)
        {
            var indexDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SearchIndex");
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            var indexWriter = new IndexWriter(FSDirectory.Open(new DirectoryInfo(indexDirectory)),
                analyzer, true, IndexWriter.MaxFieldLength.LIMITED);
            
            // Get all drivers
            foreach (var drive in DriveInfo.GetDrives())
            {
                ICollection<DirectoryInfo> directories;
                try
                {
                    directories = drive.RootDirectory.GetDirectories();
                }
                catch
                {
                    continue;
                }
                CreateIndexForAllFiles(directories, indexWriter);
            }
            indexWriter.Close();
            MessageBox.Show("Finished");
        }

        private void CreateIndexForAllFiles(ICollection<DirectoryInfo> directories, IndexWriter indexWriter)
        {
            foreach (var directory in directories)
            {
                // Index current directory
                Document document = new Document();
                document.Add(new Field("Name", directory.Name, Field.Store.YES, Field.Index.ANALYZED));
                string fullName = string.Empty;
                bool failedToGetFullName = false;
                try
                {
                    fullName = directory.FullName;
                }
                catch
                {
                    failedToGetFullName = true;
                }
                if (!failedToGetFullName)
                {
                    document.Add(new Field("Path", directory.FullName, Field.Store.YES, Field.Index.ANALYZED));
                }
                document.Add(new Field("Type", "Directory", Field.Store.YES, Field.Index.ANALYZED));
                indexWriter.AddDocument(document);     
           
                // Index all files
                ICollection<FileInfo> files = new Collection<FileInfo>();
                bool failedToGetFiles = false;
                try
                {
                    files = directory.GetFiles();
                }
                catch
                {
                    failedToGetFiles = true;
                }
                finally
                {
                    if (!failedToGetFiles)
                    {
                        foreach (var file in files)
                        {
                            Document documentFile = new Document();
                            documentFile.Add(new Field("Name", file.Name, Field.Store.YES, Field.Index.ANALYZED));
                            string fileFullName = string.Empty;
                            bool failedToGetFileFullName = false;
                            try
                            {
                                fileFullName = file.FullName;
                            }
                            catch
                            {
                                failedToGetFileFullName = true;
                            }
                            finally
                            {
                                if (!failedToGetFileFullName)
                                {
                                    documentFile.Add(new Field("Path", fileFullName, Field.Store.YES, Field.Index.ANALYZED));
                                }
                            }
                            documentFile.Add(new Field("Type", "File", Field.Store.YES, Field.Index.ANALYZED));
                            indexWriter.AddDocument(documentFile);
                        }
                    }
                }
                

                // Index All directories
                ICollection<DirectoryInfo> childDirectories = new Collection<DirectoryInfo>();
                bool failToLoadDirectories = false;
                try
                {
                    childDirectories = directory.GetDirectories();
                }
                catch
                {
                    failToLoadDirectories = true;
                }
                finally
                {
                    if (!failToLoadDirectories)
                    {
                        CreateIndexForAllFiles(childDirectories, indexWriter);
                    }
                }
            }
        }
    }
}
