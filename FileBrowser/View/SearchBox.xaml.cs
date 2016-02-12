using FileBrowser.Search;
using FileBrowser.ViewModel;
using Lucene.Net.Documents;
using SmartFramework.Core.Command;
using System;
using System.Collections.Generic;
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
    /// SearchBox.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBox : UserControl
    {
        private SearchBoxViewModel _searchBoxViewModel;
        public FileItemsView FileItemsView { get; set; }
        public SearchBox()
        {
            InitializeComponent();
            _searchBoxViewModel = new SearchBoxViewModel();
            DataContext = _searchBoxViewModel;
            var commandBinding = new CommandBinding();
            commandBinding.Command = SearchCommand;
            commandBinding.CanExecute += commandBinding_CanExecute;
            commandBinding.Executed += commandBinding_Executed;
            SearchCommand.InputGestures.Add(new KeyGesture(Key.Enter));
            CommandBindings.Add(commandBinding);
        }

        void commandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var hits = _searchBoxViewModel.Searcher.Search(SearchText.Text);
            var fileSystemItems = hits.Select(item => new FileSystemItem(item.Path, item.Name, item.FileItemType)).ToList();
            FileBrowserHistory.PushBackHistory(FileBrowserHistory.SearchPage);
            FileItemsView.SetDataContext(new FileItemsViewModel(fileSystemItems));
            // MessageBox.Show("Search" + _searchBoxViewModel.Searcher.SearchPurpose + _searchBoxViewModel.Searcher.SerchScope);
            // e.Handled = true;
        }

        void commandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SearchText.Text != string.Empty;
            e.Handled = true;
        }

        public static RoutedCommand SearchCommand = new RoutedCommand();

        private void SearchScopeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchScopeComboBox.SelectedIndex == 0)
                _searchBoxViewModel.SelectedSearchScope = SearchScope.AllDisk;
            else
                _searchBoxViewModel.SelectedSearchScope = SearchScope.CurrentDirectory;
        }

        private void SearchPurposeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchPurposeComboBox.SelectedIndex == 0)
                _searchBoxViewModel.SelectedSearchPurpose = SearchPurpose.Name;
            else
                _searchBoxViewModel.SelectedSearchPurpose = SearchPurpose.Color;
        }

        private static void Search(object parameter, IInputElement target)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }
    }
}
