using PlatformFramework.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.Design;
using PlatformFramework.Shell.LeftPanel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using PlatformFramework.Shell;
using PlatformFramework.Core.Composition;
using PlatformFramework.Shell.MainSite;

namespace PlatformFramework
{
    public class ShellApplication : Application
    {
        private StudioWindow _mainWindow;
        
        public CompositionHost Host = CompositionHost.Instance();

        private FrameworkElement _mainSite;
        public FrameworkElement MainSite
        {
            get { return _mainSite; }
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);            
            Initialize();     
        }

        private void Initialize()
        { 
            _mainWindow = new StudioWindow();
            _mainSite = _mainWindow.MainSite;
            // Initialize Composition Host
            Host.InitiailizeComposition(this);
            // Initialize main sites
            MainSiteManager.Instance().InitializeMainSite(this);
            // Initialize left panels
            LeftPanelManager.Instance().InitializeLeftPanels(this, _mainWindow.LeftPanelsContainer); 
            _mainWindow.Show();           
        }
    }
}
