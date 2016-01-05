using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartFramework.Controls
{
    public class ShellTreeViewItem : TreeViewItem
    {
        static ShellTreeViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShellTreeViewItem), new FrameworkPropertyMetadata(typeof(ShellTreeViewItem)));
        }

        /// <summary>
        /// Our depth.
        /// </summary>
        public int Depth { get; set; }

        /// <inheritdoc />
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ShellTreeViewItem() { Depth = Depth + 1 };
        }

    }
}
