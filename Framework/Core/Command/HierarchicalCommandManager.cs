using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformFramework.Core.Command
{
    public abstract class HierarchicalCommandManager
    {
        /// <summary>
        /// All of the commands which have been added to the hierarchy
        /// </summary>
        protected Dictionary<ICommandEx, ICommandSourceEx> AddedCommands { get; private set; }

        /// <summary>
        /// Gets a parent item for a command to add to the hierarchy
        /// </summary>
        /// <param name="command">the command of interest</param>
        /// <returns>the parent command or null of the command should not be in the hierarchy</returns>
        protected abstract ICommandEx GetPath(ICommandEx command);

        /// <summary>
        /// Creates a visualization of a command that will accept children
        /// </summary>
        /// <param name="command">the command to create a visualiation for</param>
        /// <returns>the command visual</returns>
        protected abstract ICommandSourceParent CreateParentCommandVisual(ICommandEx command);

        /// <summary>
        /// Creates a visualiation of a leaf command
        /// </summary>
        /// <param name="command">The command to create a visualization for</param>
        /// <returns>the newly created visualization of a leaf element</returns>
        protected abstract ICommandSourceEx CreateLeafCommandVisual(ICommandEx command);
    }
}
