using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformFramework.Core.Command
{
    public interface ICommandVisualCreator
    {
        ICommandSourceEx CreateVisual(); 
    }
}
