using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DefaultMainSite = SmartFramework.Shell.MainSite.Default;

namespace SmartFramework.Shell.LeftPanel
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
