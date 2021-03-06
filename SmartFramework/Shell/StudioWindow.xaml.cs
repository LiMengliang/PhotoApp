﻿using SmartFramework.Controls;
using SmartFramework.Core.Menu;
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
using System.Windows.Shapes;

namespace SmartFramework.Shell
{
    /// <summary>
    /// Interaction logic for StudioWindow.xaml
    /// </summary>
    public partial class StudioWindow : Window
    {
        private MenuManager _menuManagr;
        public Application Application { get; private set; }

        public ShellTabControl LeftPanelsContainer
        {
            get { return LeftPanels; }
        }

        public StudioWindow(Application application)
        {
            InitializeComponent();
            _menuManagr = new MenuManager(ApplicationMenu);
            _menuManagr.BuildMenu();
            Application = application;
        }
    }
}
