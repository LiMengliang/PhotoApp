using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartFramework.Shell.MainSite
{
    public class MainSiteManager
    {
        private static MainSiteManager _mainSiteManager;
        private ShellApplication _shellApplication;

        [ImportMany(typeof(IMainSite))]
        private IEnumerable<Lazy<IMainSite>> _allMainSites;

        private IMainSite _currentVisibleSite;

        public static MainSiteManager Instance()
        {
            if (_mainSiteManager == null)
            {
                _mainSiteManager = new MainSiteManager();
            }
            return _mainSiteManager;
        }

        public void InitializeMainSite(ShellApplication application)
        {
            _shellApplication = application;
        }

        public void SetVisibleSite(IMainSite mainSite)
        {
            if (_currentVisibleSite != mainSite)
            {
                if (_currentVisibleSite != null)
                {
                    _currentVisibleSite.RootFrameworkElement.Visibility = Visibility.Collapsed;
                }                
                _currentVisibleSite = mainSite;
                if (!(_shellApplication.MainSite as Grid).Children.Contains(_currentVisibleSite.RootFrameworkElement))
                {
                    (_shellApplication.MainSite as Grid).Children.Add(_currentVisibleSite.RootFrameworkElement);
                }                
                _currentVisibleSite.RootFrameworkElement.Visibility = Visibility.Visible;
            }
        }
    }
}
