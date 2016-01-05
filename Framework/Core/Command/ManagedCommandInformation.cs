using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformFramework.Core.Command
{
    public class ManagedCommandInformation
    {
        public ICommandSourceEx Visual { get; private set; }

        public ICommandEx Command { get; private set; }

        public ICommandSourceParent ParentVisual { get; private set; }

        public ManagedCommandInformation(ICommandSourceEx visual, ICommandEx command)
        {
            Visual = visual;
            Command = command;
        }
    }
}
