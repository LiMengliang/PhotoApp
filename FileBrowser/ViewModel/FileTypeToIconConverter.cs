using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileBrowser.ViewModel
{
    public class FileTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FileItemType type = (FileItemType)value;
            switch (type)
            {
                case FileItemType.File: return "../Resources/File.png";
                case FileItemType.Directory: return "../Resources/Folder.png";
                case FileItemType.Drive: return "../Resources/Driver.png";      
                default:return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
