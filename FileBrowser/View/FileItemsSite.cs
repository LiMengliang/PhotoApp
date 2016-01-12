using SmartFramework;
using SmartFramework.Shell.MainSite;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileBrowser.View
{
    [Export(typeof(IMainSite))]
    public class FileItemsSite : IMainSite
    {
        private FileItemsView _fileItemsView;

        public FileItemsSite()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (_fileItemsView == null)
            {
                _fileItemsView = new FileItemsView();
            }
        }

        public FrameworkElement RootFrameworkElement
        {
            get { return _fileItemsView; }
        }
    }
}
