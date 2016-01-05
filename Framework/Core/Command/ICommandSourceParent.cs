using PlatformFramework.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformFramework.Core.Command
{
    public interface ICommandSourceParent : ICommandSourceEx
    {
        /// <summary>
        /// Add a child command to display
        /// </summary>
        /// <param name="item">the command visual to display</param>
        void AddItem(ICommandSourceEx item);

        /// <summary>
        /// Inserts a command for display
        /// </summary>
        /// <param name="item">The command visual to display</param>
        /// <param name="insertIndex">insert index</param>
        void InsertItem(ICommandSourceEx item, int insertIndex);

        /// <summary>
        /// Removes a command visual from the child display list
        /// </summary>
        /// <param name="item">The item to remove</param>
        void RemoveItem(ICommandSourceEx item);

        /// <summary>
        /// Gets the label of this parent item.
        /// </summary>
        string Label { get; }
    }
}
