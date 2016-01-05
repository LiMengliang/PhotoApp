using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlatformFramework.Shell.MainSite
{
    public interface IMainSite
    {
        void Initialize();

        FrameworkElement RootFrameworkElement { get; }
    }
}
