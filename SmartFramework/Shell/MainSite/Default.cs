using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmartFramework.Shell.MainSite
{
    [Export(typeof(IMainSite))]
    public class Default : IMainSite
    {
        public void Initialize()
        {
            return;
        }

        public System.Windows.FrameworkElement RootFrameworkElement
        {
            get { return new Grid(); }
        }
    }
}
