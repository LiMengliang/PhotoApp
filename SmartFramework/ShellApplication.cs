using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.Design;
using SmartFramework.Shell;
using SmartFramework.Core.Composition;
using SmartFramework.Shell.MainSite;
using SmartFramework.Shell.LeftPanel;
using System.Windows.Input;

namespace SmartFramework
{
    public class ShellApplication : Application
    {
        public StudioWindow MainWindow { get; private set; }
        
        public static CompositionHost Host = CompositionHost.Instance();

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
            MainWindow = new StudioWindow(this);
            _mainSite = MainWindow.MainSite;
            // Initialize Composition Host
            Host.InitiailizeComposition(this);
            // Initialize main sites
            MainSiteManager.Instance().InitializeMainSite(this);
            // Initialize left panels
            LeftPanelManager.Instance().InitializeLeftPanels(this, MainWindow.LeftPanelsContainer);
            MainWindow.Show();
            
        }        
    }
}
