using FileBrowser.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FileBrowser.View;

namespace FileBrowser.ViewModel
{
    public class FileItemsViewModel : DependencyObject
    {
        private DirectoryRecord _directoryRecord;
        public DirectoryRecord DirectoryRecord
        {
            get
            {
                return _directoryRecord;
            }
            set
            {
                _directoryRecord = value;
                CurrentDirectory = _directoryRecord.Info.FullName;
                var allItems = value.Directories.Select(directory => new FileSystemItem(directory.Info.FullName, directory.Info.Name, FileItemType.Directory)).ToList();
                allItems.AddRange(value.Files.Select(file => new FileSystemItem(file.FullName, file.Name, FileItemType.File)));
                _directoriesAndFiles = new ObservableCollection<FileSystemItem>(allItems);
            }
        }

        public string CurrentDirectory
        {
            get { return (string)GetValue(CurrentDirectoryProperty); }
            set { SetValue(CurrentDirectoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDirectory.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDirectoryProperty =
            DependencyProperty.Register("CurrentDirectory", typeof(string), typeof(FileItemsViewModel), new PropertyMetadata(string.Empty));        

        private ObservableCollection<FileSystemItem> _directoriesAndFiles;
        public ObservableCollection<FileSystemItem> DirectoriesAndFiles
        {
            get 
            {
                return _directoriesAndFiles;
            }
        }

        public FileItemsViewModel(DirectoryRecord directoryRecord)
        {
            DirectoryRecord = directoryRecord;
        }

        public FileItemsViewModel()
        {
            var drivers = DriveInfo.GetDrives();
            var directories = drivers.Select(info => new FileSystemItem(info.Name, info.Name, FileItemType.Drive));
            _directoriesAndFiles = new ObservableCollection<FileSystemItem>(directories);
        }
    }
}
