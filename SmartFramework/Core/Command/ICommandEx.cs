using SmartFramework.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartFramework.Core.Command
{
    /// <summary>
    /// Defines a situation in which a command will be displayed to the user
    /// </summary>
    public enum CommandDisplaySituation
    {
        /// <summary>
        /// Unknown display situatin
        /// </summary>
        Unknown,

        /// <summary>
        /// For display in the Application Menu
        /// </summary>
        ApplicationMenu,

        /// <summary>
        /// For display in an adorner
        /// </summary>
        Adorner,

        /// <summary>
        /// For display in a toolbar
        /// </summary>
        ToolBar
    }

    /// <summary>
    /// Extented ICommand interface
    /// </summary>
    public interface ICommandEx : ICommand
    {
        string UniqueId { get; set; }

        string LabelTitle { get; set; }

        ImageSource SmallIcon { get; set; }

        ImageSource LargeIcon { get; set; }

        ICommandEx MenuCommandParent { get; set; }

        ICommandEx PopupCommandParent { get; set; }

        int Weight { get; set; }

        ShellKeyGesture KeyGuesture { get; set; }

        void AttacheVisualElement();

        void DetachVisualElement();

        ICommandSourceEx CreateDefaultVisual(CommandDisplaySituation situation);
    }
}
