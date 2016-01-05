using FileBrowser.Model;
using FileBrowser.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileBrowser.ViewModel
{
    public class DirectoryRecordViewModel : DependencyObject
    {
        private DirectoryRecord _directoryRecord;
        public DirectoryRecord DirectoryRecord
        {
            get { return _directoryRecord; }            
        }

        public DirectoryRecordViewModel(DirectoryRecord directoryRecord)
        {
            _directoryRecord = directoryRecord;
            if (_directoryRecord.IsDriver)
            {
                DirectoryName = string.Format(LocalizedStrings.LocalDisk, _directoryRecord.Info.Name.Substring(0, 1));
            }
            else
            {
                DirectoryName = _directoryRecord.Info.Name;
            }
        }

        public string DirectoryName
        {
            get { return (string)GetValue(DirectoryNameProperty); }
            set { SetValue(DirectoryNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DirectoryName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DirectoryNameProperty =
            DependencyProperty.Register("DirectoryName", typeof(string), typeof(DirectoryRecordViewModel), new PropertyMetadata(string.Empty));

        public ObservableCollection<DirectoryRecordViewModel> Directories
        {
            get 
            {
                return new ObservableCollection<DirectoryRecordViewModel>(_directoryRecord.Directories.Select(directory => new DirectoryRecordViewModel(directory)));
            }
        }        
    }
}
