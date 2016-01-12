using FileBrowser.Resources;
using FileBrowser.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FileBrowser.ViewModel
{   
    public class SelectedIndexToSearchPurposeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((SearchPurpose)value == SearchPurpose.Name)
            {
                return 0;
            }
            if ((SearchPurpose)value == SearchPurpose.Color)
            {
                return 1;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((int)value == 0)
            {
                return SearchPurpose.Name;
            }
            if ((int)value == 1)
            {
                return SearchPurpose.Color;
            }
            return null;
        }
    }

    public class SelectedIndexToSearchScopeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((SearchScope)value == SearchScope.AllDisk)
            {
                return 0;
            }
            if ((SearchScope)value == SearchScope.CurrentDirectory)
            {
                return 1;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((int)value == 0)
            {
                return SearchScope.AllDisk;
            }
            if ((int)value == 1)
            {
                return SearchScope.CurrentDirectory;
            }
            return null;
        }
    }

    public class SearchBoxViewModel : DependencyObject
    {
        public Searcher Searcher { get; private set; }

        public ObservableCollection<string> DisplaySearchPurpose
        {
            get;
            set;
        }

        public ObservableCollection<string> DisplaySearchScope
        {
            get;
            set;
        }

        public SearchPurpose SelectedSearchPurpose
        {
            get { return (SearchPurpose)GetValue(SelectedSearchPurposeProperty); }
            set
            {
                SetValue(SelectedSearchPurposeProperty, value);
                if (Searcher != null)
                {
                    Searcher.SearchPurpose = value;
                }
            }
        }

        // Using a DependencyProperty as the backing store for SelectedSearchPurpose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedSearchPurposeProperty =
            DependencyProperty.Register("SelectedSearchPurpose", typeof(SearchPurpose), typeof(SearchBoxViewModel), new PropertyMetadata(SearchPurpose.Name));



        public SearchScope SelectedSearchScope
        {
            get { return (SearchScope)GetValue(SelectedSearchScopeProperty); }
            set
            {
                SetValue(SelectedSearchScopeProperty, value);
                if (Searcher != null)
                {
                    Searcher.SerchScope = value;
                }
            }
        }

        // Using a DependencyProperty as the backing store for SelectedSearchScope.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedSearchScopeProperty =
            DependencyProperty.Register("SelectedSearchScope", typeof(SearchScope), typeof(SearchBoxViewModel), new PropertyMetadata(SearchScope.CurrentDirectory));

              

        public SearchBoxViewModel()
        {
            Searcher = new Searcher();
            DisplaySearchPurpose = new ObservableCollection<string>(new List<string>
                {
                    LocalizedStrings.SearchName,
                    LocalizedStrings.SearchColor
                });
            DisplaySearchScope = new ObservableCollection<string>(new List<string>
                {
                    LocalizedStrings.AllDisck,
                    LocalizedStrings.CurrentDirectory
                });
            SelectedSearchPurpose = SearchPurpose.Name;
            SelectedSearchScope = SearchScope.CurrentDirectory;
        }
    }
}
