using SmartFramework.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartFramework.Controls
{
    public class ShellMenuItem : MenuItem, ICommandSourceParent
    {
        private ICommandEx _commandEx;
        public ICommandEx CommandEx 
        {
            get
            {
                return _commandEx;
            }
            set
            { 
                Command = value as ICommand;
                _commandEx = value;
            }
        }

        public IInputElement CommandTarget { get; set; }

        public ShellMenuItem(ICommandEx command)
        {
            CommandEx = command;
            
            Header = CommandEx.LabelTitle;
        }

        public void AddItem(ICommandSourceEx item)
        {
            Items.Add(item);
        }

        public void InsertItem(ICommandSourceEx item, int insertIndex)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(ICommandSourceEx item)
        {
            throw new NotImplementedException();
        }

        public string Label
        {
            get { return CommandEx.LabelTitle; }
        }
    }
}
