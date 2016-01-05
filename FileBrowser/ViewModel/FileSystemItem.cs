using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FileBrowser.ViewModel
{
    public enum FileItemType
    {
        File, Directory, Drive
    }

    public class FileSystemItem : DependencyObject
    {
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(FileSystemItem), new PropertyMetadata(String.Empty));

        public string FullName { get; private set; }

        public FileItemType Type { get; private set; }

        public FileSystemItem(string fullName, string name, FileItemType type)
        {
            FullName = fullName;
            Name = name;
            Type = type;
        }



        public BitmapFrame Icon
        {
            get { return (BitmapFrame)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(BitmapFrame), typeof(FileSystemItem), null);

                
    }
}
