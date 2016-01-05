using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlatformFramework.Core.Command
{
    public interface ICommandSourceEx
    {
        ICommandEx CommandEx { get; set; }

        object CommandParameter { get; set; }

        IInputElement CommandTarget { get; set; }
    }
}
