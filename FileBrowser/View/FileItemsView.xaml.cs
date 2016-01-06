using FileBrowser.Model;
using FileBrowser.ViewModel;
using SmartFramework.Core.Command;
using System;
using System.Collections.Generic;
using System.IO;
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
using FileBrowser.Search;
using Lucene.Net.Documents;

namespace FileBrowser.View
{
    /// <summary>
    /// Interaction logic for FileItemsView.xaml
    /// </summary>
    public partial class FileItemsView : UserControl
    {
        private ImageLoader _imageLoader;
        public FileItemsView()
        {
            InitializeComponent();
            BackButton.Command = BackCommand;
            ForwardButton.Command = ForwardCommand;
            // Create search index
            var searchIndexCreator = new IndexCreator(Dispatcher);
            // searchIndexCreator.CreateSearchIndex();
            _imageLoader = new ImageLoader(Dispatcher);
            Unloaded += EndLoadingImage;
        }

        public static readonly ShellRelayCommand BackCommand = new ShellRelayCommand(BackCommandHandler, (x) => FileBrowserHistory.BackHistoryCount() > 1);

        public static readonly ShellRelayCommand ForwardCommand = new ShellRelayCommand(ForwardCommandHandler, (x) => FileBrowserHistory.ForwardHistoryCount() > 0);

        public void OnItemDoubleClicked(object sender, MouseEventArgs args)
        {
            var selectedFileItem = (sender as FrameworkElement).DataContext as FileSystemItem;
            if (selectedFileItem.Type == FileItemType.Directory || selectedFileItem.Type == FileItemType.Drive)
            {
                FileBrowserHistory.PushBackHistory(selectedFileItem.FullName);
                if (FileBrowserHistory.ForwardHistoryCount() > 0 && selectedFileItem.FullName == FileBrowserHistory.GetTopForwardHistory())
                {
                    FileBrowserHistory.PopForwardHistory();
                }
                SetDataContext(new FileItemsViewModel(new DirectoryRecord() { Info = new DirectoryInfo(selectedFileItem.FullName)}));
            }
        }

        public void LoadImagesAsync()
        {
            _imageLoader.LoadImagesAsync(this.DataContext as FileItemsViewModel);
        }

        private void EndLoadingImage(object sender, RoutedEventArgs args)
        {
            _imageLoader.EndLoadingImages();
        }

        private static void BackCommandHandler(object parameter)
        {
            var filesItemView = parameter as FileItemsView;
            FileBrowserHistory.PushForwardHistory(FileBrowserHistory.PopBackHistory());
            var lastDirectory = FileBrowserHistory.GetTopBackHistory();
            if (lastDirectory == String.Empty)
            {
                filesItemView.SetDataContext(new FileItemsViewModel());
            }
            else
            {
                filesItemView.SetDataContext(new FileItemsViewModel(new DirectoryRecord() { Info = new DirectoryInfo(lastDirectory) }));
            }
        }

        public void SetDataContext(FileItemsViewModel viewModel)
        {
            DataContext = viewModel;
            LoadImagesAsync();
        }

        private static void ForwardCommandHandler(object parameter)
        {
            var filesItemView = parameter as FileItemsView;
            var lastDirectory = FileBrowserHistory.GetTopForwardHistory();
            if (lastDirectory == String.Empty)
            {
                filesItemView.SetDataContext(new FileItemsViewModel());
            }
            else
            {
                filesItemView.SetDataContext(new FileItemsViewModel(new DirectoryRecord() { Info = new DirectoryInfo(lastDirectory) }));
            }
            FileBrowserHistory.PushBackHistory(FileBrowserHistory.PopForwardHistory());
            // filesItemView.LoadImagesAsync();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searcher = new Searcher(@"E:\GitHub Project\Everything\PhotoApplication\bin\Debug\SearchIndex");
            var hits = searcher.Search(SearchInupt.Text);
            var fileSystemItems = new List<FileSystemItem>();
            for (int i = 0; i < hits.Count(); i++)
            {
                var hit = hits[i];
                Document doc = searcher.GetDocument(hit.Doc);
                Field fileNameField = doc.GetField("Name");
                Field pathField = doc.GetField("Path");
                Field typeField = doc.GetField("Type");
                var fileItemType = FileItemType.File;
                switch (typeField.StringValue)
                { 
                    case "File":
                        fileItemType = FileItemType.File;
                        break;
                    case "Directory":
                        fileItemType = FileItemType.Directory;
                        break;
                    case "Driver":
                        fileItemType = FileItemType.Drive;
                        break;

                }
                fileSystemItems.Add(new FileSystemItem(pathField.StringValue, fileNameField.StringValue, fileItemType));
            }

            FileBrowserHistory.PushBackHistory(FileBrowserHistory.SearchPage);
            SetDataContext(new FileItemsViewModel(fileSystemItems));
        }
    }
}
