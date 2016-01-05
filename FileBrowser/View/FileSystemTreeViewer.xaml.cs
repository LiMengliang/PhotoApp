using FileBrowser.Model;
using FileBrowser.ViewModel;
using SmartFramework.Shell.LeftPanel;
using SmartFramework.Shell.MainSite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileBrowser.View
{
    /// <summary>
    /// Interaction logic for FileSystemTreeViewer.xaml
    /// </summary>
    public partial class FileSystemTreeViewer : UserControl
    {
        private ILeftPanel _fileSystemTreePanel;

        public FileSystemTreeViewer(ILeftPanel fileSystemTreePanel)
        {
            InitializeComponent();
            var directories = FileSystemUtilities.GetRootDirectories();
            TreeViewer.DataContext = directories;
            _fileSystemTreePanel = fileSystemTreePanel;
            var fileItemsViewModel = new FileItemsViewModel();
            MainSiteManager.Instance().SetVisibleSite(_fileSystemTreePanel.MainSite);
            (_fileSystemTreePanel.MainSite.RootFrameworkElement as FileItemsView).SetDataContext(fileItemsViewModel);
            FileBrowserHistory.PushBackHistory(String.Empty);
        }

        /// <summary>
        /// Double click on tree view item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnDirectoryDoubleClicked(object sender, MouseButtonEventArgs args)
        {            
            var currentItem = sender as TreeViewItem;
            if (!currentItem.IsSelected)
            {
                return;
            }
            var selectedDirectory = currentItem.DataContext as DirectoryRecordViewModel;
            var fileItemsViewModel = new FileItemsViewModel(selectedDirectory.DirectoryRecord);
            MainSiteManager.Instance().SetVisibleSite(_fileSystemTreePanel.MainSite);
            (_fileSystemTreePanel.MainSite.RootFrameworkElement as FileItemsView).SetDataContext(fileItemsViewModel);

            FileBrowserHistory.PushBackHistory(selectedDirectory.DirectoryRecord.Info.FullName);
            args.Handled = true;
        }
    }

    public static class TreeViewItemExtensions
    {
        public static int GetDepth(this TreeViewItem item)
        {
            TreeViewItem parent;
            while ((parent = GetParent(item)) != null)
            {
                return GetDepth(parent) + 1;
            }
            return 0;
        }

        private static TreeViewItem GetParent(TreeViewItem item)
        {
            var parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as TreeViewItem;
        }
    }

    public class LeftMarginMultiplierConverter : IValueConverter
    {
        public double Length { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null)
                return new Thickness(0);

            return new Thickness(Length * item.GetDepth(), 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
