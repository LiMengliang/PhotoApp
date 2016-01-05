using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DefaultMainSite = PlatformFramework.Shell.MainSite.Default;

namespace PlatformFramework.Shell.LeftPanel
{
    [Export(typeof(ILeftPanel))]
    public class Default : ILeftPanel
    {
        public string Lable
        {
            get { return "DEFAULT"; }
        }

        public MainSite.IMainSite MainSite
        {
            get { return new DefaultMainSite(); }
        }

        public void Initialize()
        {
            return;
        }

        public System.Windows.FrameworkElement Content
        {
            get { return new Button(); }
        }
    }
}
