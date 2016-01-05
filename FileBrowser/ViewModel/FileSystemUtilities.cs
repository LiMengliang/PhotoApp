using FileBrowser.Model;
using FileBrowser.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileBrowser.Resources;

namespace FileBrowser.ViewModel
{
    public class FileSystemUtilities
    {
        public static ObservableCollection<DirectoryRecordViewModel> GetRootDirectories()
        {
            var directoreies = new ObservableCollection<DirectoryRecordViewModel>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                directoreies.Add(
                    new DirectoryRecordViewModel(
                    new DirectoryRecord
                    {
                        Info = new DirectoryInfo(drive.Name),
                        IsDriver = true
                    })
                );
            }
            return directoreies;
        }
    }
}
