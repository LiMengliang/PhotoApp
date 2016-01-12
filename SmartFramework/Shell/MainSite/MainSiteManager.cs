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
        private ShellApplication ShellApplication { get; set; }

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
            ShellApplication = application;
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
                if (!(ShellApplication.MainSite as Grid).Children.Contains(_currentVisibleSite.RootFrameworkElement))
                {
                    (ShellApplication.MainSite as Grid).Children.Add(_currentVisibleSite.RootFrameworkElement);
                }                
                _currentVisibleSite.RootFrameworkElement.Visibility = Visibility.Visible;
            }
        }
    }
}
