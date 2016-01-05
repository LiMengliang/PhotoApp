using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartFramework.Controls
{
    public class ShellTextBox : TextBox
    {
        public ShellTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShellTextBox), new FrameworkPropertyMetadata(typeof(ShellTextBox)));
        }
    }
}
