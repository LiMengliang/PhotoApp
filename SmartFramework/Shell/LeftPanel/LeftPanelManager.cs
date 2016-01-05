using SmartFramework.Controls;
using SmartFramework.Shell.MainSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartFramework.Shell.LeftPanel
{
    public class LeftPanelManager
    {
        private ShellApplication _shellApplication;
        private static LeftPanelManager _instance;
        private IList<Lazy<ILeftPanel>> _leftPanels;
        private ILeftPanel _selectedPanel;
        public static LeftPanelManager Instance()
        {
            if (_instance == null)
            {
                _instance = new LeftPanelManager();
            }
            return _instance;
        }

        public void InitializeLeftPanels(ShellApplication application, TabControl tabControl)
        {
            _shellApplication = application;
            tabControl.SelectionChanged += InitializeSelectedPanel;

            _leftPanels = application.Host.GetExports<ILeftPanel>().ToList();
            foreach (var leftPanel in _leftPanels)
            {
                var tabItem = new ShellTabItem();
                tabItem.Header = leftPanel.Value.Lable;
                tabItem.Content = leftPanel.Value.Content;
                // TODO: should make binding
                tabControl.Items.Add(tabItem);
            }
            tabControl.SelectedItem = tabControl.Items[0];
        }

        private void InitializeSelectedPanel(object sender, SelectionChangedEventArgs args)
        {
            _selectedPanel = _leftPanels.ElementAt((sender as TabControl).SelectedIndex).Value;
            MainSiteManager.Instance().SetVisibleSite(_selectedPanel.MainSite);
        }
    }
}
