using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartFramework.Controls
{
    public class ShellButton : Button
    {
        static ShellButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShellButton), new FrameworkPropertyMetadata(typeof(ShellButton)));
        }
    }
}
