using SmartFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApplication
{    
    public class PhotoApplication : ShellApplication
    {
        [STAThread]
        public static void Main()
        {
            var photoApplication = new PhotoApplication();
            photoApplication.Run();
        }
    }
}
