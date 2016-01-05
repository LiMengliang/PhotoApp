using SmartFramework.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFramework.Core.Menu
{
    public class MenuPathCommands
    {
        public static readonly ICommandEx FileCommand = new ShellRelayCommand(MenuPathCommands.EmptyExecuteHandler, MenuPathCommands.DefaultCanExecute)
        {
            UniqueId = "PlatformFramework.Menu.File",
            LabelTitle = LocalizedStrings.FileMenuName,
        };

        public static readonly ICommandEx NewCommand = new ShellRelayCommand(MenuPathCommands.EmptyExecuteHandler, MenuPathCommands.DefaultCanExecute)
        {
            UniqueId = "PlatformFramework.Menu.File.New",
            LabelTitle = LocalizedStrings.NewMenuName,
            MenuCommandParent = FileCommand
        };

        public static readonly ICommandEx NewFileCommand = new ShellRelayCommand(MenuPathCommands.EmptyExecuteHandler, (x)=>false)
        {
            UniqueId = "PlatformFramework.Menu.File.New.NewFile",
            LabelTitle = LocalizedStrings.NewFileName,
            MenuCommandParent = NewCommand
        };

        public static readonly ICommandEx Exit = new ShellRelayCommand(MenuPathCommands.EmptyExecuteHandler, MenuPathCommands.DefaultCanExecute)
        {
            UniqueId = "PlatformFramework.Menu.Exit",
            LabelTitle = LocalizedStrings.ExitMenuName,
            MenuCommandParent = FileCommand,
        };            

        public static readonly ICommandEx HelpCommand = new ShellRelayCommand(MenuPathCommands.EmptyExecuteHandler, MenuPathCommands.DefaultCanExecute)
        {
            UniqueId = "PlatformFramework.Menu.Hlep",
            LabelTitle = LocalizedStrings.HelpMenuName,
        };

        public static readonly ICommandEx AboutCommand = new ShellRelayCommand(MenuPathCommands.EmptyExecuteHandler, MenuPathCommands.DefaultCanExecute)
        {
            UniqueId = "PlatformFramework.Menu.Hlep.About",
            LabelTitle = LocalizedStrings.AboutMenuName,
            MenuCommandParent = HelpCommand
        };

        private static void EmptyExecuteHandler(object commandParameter)
        {
        }

        private static bool DefaultCanExecute(object commandParameter)
        {
            return true;    
        }
    }
}
