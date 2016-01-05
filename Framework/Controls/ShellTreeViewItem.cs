using PlatformFramework.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PlatformFramework.Controls
{
    public class ShellTreeViewItem : TreeViewItem
    {
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
