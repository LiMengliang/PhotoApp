using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Model
{
    public class DirectoryRecord
    {
        public DirectoryInfo Info { get; set; }
        public bool IsDriver { get; set; }

        private ICollection<string> _imageExtensions = new Collection<string>
        {
            ".bmp", ".jpg", ".jpeg", ".png", ".gif",
            ".BMP", ".JPG", ".JPEG", ".PNG", ".GIF"
        };

        public IEnumerable<FileInfo> Files
        {
            get
            {
                return Info.GetFiles().Where(fileInfo => _imageExtensions.Any(extension => extension == fileInfo.Extension));
            }
        }

        public IEnumerable<DirectoryRecord> Directories
        {
            get 
            {
                DirectoryInfo[] directoriesInfo = null;
                var directories = new List<DirectoryRecord>();
                try
                {
                    directoriesInfo = Info.GetDirectories("*", SearchOption.TopDirectoryOnly);
                }
                catch
                {
                }
                finally 
                {
                    if (directoriesInfo != null)
                    {                    
                        directories = (from directoryInfo in directoriesInfo 
                               select new DirectoryRecord
                        {
                            Info = directoryInfo
                        }).ToList();
                    }
                }
                return directories;
            }
        }
    }
}
