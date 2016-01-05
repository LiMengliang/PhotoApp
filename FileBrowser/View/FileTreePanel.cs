using FileBrowser.Resources;
using SmartFramework.Shell.LeftPanel;
using SmartFramework.Shell.MainSite;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FileBrowser.View
{
    [Export(typeof(ILeftPanel))]
    public class FileTreePanel : ILeftPanel
    {
        private FileItemsSite _fileItemsSite;

        public FileTreePanel()
        {
            Initialize();
        }

        public string Lable
        {
            get
            {
                return LocalizedStrings.FileTreePage;
            }
        }

        public System.Windows.FrameworkElement Content
        {
            get
            {
                var fileSystemTreeViewer = new FileSystemTreeViewer(this);
                return fileSystemTreeViewer;
            }
        }

        public void Initialize()
        {
            _fileItemsSite = new FileItemsSite();
        }

        public IMainSite MainSite
        {
            get { return _fileItemsSite; }
        }
    }
}
