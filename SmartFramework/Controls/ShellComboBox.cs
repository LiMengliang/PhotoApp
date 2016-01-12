using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartFramework.Controls
{
    public class ShellComboBox : ComboBox
    {
        static ShellComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShellComboBox), new FrameworkPropertyMetadata(typeof(ShellComboBox)));
        }
    }
}
