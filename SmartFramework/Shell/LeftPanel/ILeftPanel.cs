using SmartFramework.Shell.MainSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmartFramework.Shell.LeftPanel
{
    /// <summary>
    /// Left panel interface
    /// </summary>
    public interface ILeftPanel
    {
        /// <summary>
        /// Lable of this panel
        /// </summary>
        string Lable { get; }

        IMainSite MainSite { get; }

        /// <summary>
        /// Initialize
        /// </summary>
        void Initialize();
  
        /// <summary>
        /// View of the panel.
        /// </summary>
        FrameworkElement Content { get; }
    }
}
