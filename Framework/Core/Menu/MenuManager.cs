using PlatformFramework.Core.Command;
using PlatformFramework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformFramework.Core.Menu
{
    public class MenuManager
    {
        private ShellMenu _shellMenu;

        private IDictionary<ICommandEx, ICommandSourceParent> _commandInformations;

        public MenuManager(ShellMenu applicationMenu)
        {
            _shellMenu = applicationMenu;
            _commandInformations = new Dictionary<ICommandEx, ICommandSourceParent>();
        }

        public void BuildMenu()
        {
            foreach (var command in _defaultCommand)
            {
                ICommandSourceParent commandVisual;
                if (!_commandInformations.TryGetValue(command, out commandVisual))
                {
                    commandVisual = new ShellMenuItem(command);
                    _commandInformations.Add(command, commandVisual);
                }
                var parentCommand = command.MenuCommandParent;
                if (parentCommand != null)
                {
                    BuildMenuCommandHierarchy(parentCommand, commandVisual, _shellMenu);
                }
            }
        }

        public void AddMenuCommand(ICommandEx command)
        {
        }

        private void BuildMenuCommandHierarchy(ICommandEx parentCommand, ICommandSourceParent commandVisual, ShellMenu shellMenu)
        {
            if (parentCommand != null)
            {
                ICommandSourceParent parentCommandVisual;
                if (_commandInformations.TryGetValue(parentCommand, out parentCommandVisual))
                {
                    parentCommandVisual.AddItem(commandVisual);
                    return;
                }
                parentCommandVisual = new ShellMenuItem(parentCommand);
                parentCommandVisual.AddItem(commandVisual);
                _commandInformations.Add(parentCommand, parentCommandVisual);
                parentCommand = parentCommand.MenuCommandParent;
                BuildMenuCommandHierarchy(parentCommand, parentCommandVisual, shellMenu);
            }
            else 
            {
                shellMenu.AddItem(commandVisual);
            }
        }

        private ICollection<ICommandEx> _defaultCommand = new List<ICommandEx>()
        {
            MenuPathCommands.NewFileCommand,
            MenuPathCommands.Exit,
            MenuPathCommands.AboutCommand
        };
    }
}
