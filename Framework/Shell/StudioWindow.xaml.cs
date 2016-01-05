using PlatformFramework.Controls;
using PlatformFramework.Core.Menu;
using PlatformFramework.Shell;
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

namespace PlatformFramework.Shell
{
    /// <summary>
    /// Interaction logic for StudioWindow.xaml
    /// </summary>
    public partial class StudioWindow : Window
    {
        private MenuManager _menuManagr;

        public ShellTabControl LeftPanelsContainer
        {
            get { return LeftPanels; }
        }

        public StudioWindow()
        {
            InitializeComponent();
            _menuManagr = new MenuManager(ApplicationMenu);
            _menuManagr.BuildMenu();
        }
    }
}
