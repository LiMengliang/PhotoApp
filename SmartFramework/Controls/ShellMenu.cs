using SmartFramework.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SmartFramework.Controls
{
    public class ShellMenu : Menu, ICommandSourceParent
    {
        static ShellMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShellMenu), new FrameworkPropertyMetadata(typeof(ShellMenu)));
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
            get { throw new NotImplementedException(); }
        }
        
        public ICommandEx CommandEx
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        
        public object CommandParameter
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        
        public System.Windows.IInputElement CommandTarget
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
