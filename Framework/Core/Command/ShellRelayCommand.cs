using PlatformFramework.Shell;
using System;
using System.Windows.Media;

namespace PlatformFramework.Core.Command
{
    public class ShellRelayCommand : ICommandEx
    {
        /// <summary>
        /// Delegate to handle command execution.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public delegate void ExecuteHandler(object parameter);

        /// <summary>
        /// Delegate to check if command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public delegate bool CanExecuteHandler(object parameter);

        private ICommandVisualCreator _visualCreator;
        private ExecuteHandler _executeHandler;
        private CanExecuteHandler _canExecuteHanlder;

        public ShellRelayCommand(ExecuteHandler executeHandler, CanExecuteHandler canExecuteHandler)
        {
            _executeHandler = executeHandler;
            _canExecuteHanlder = canExecuteHandler;
        }

        public string UniqueId { get; set; }

        public string LabelTitle { get; set; }

        public ImageSource SmallIcon { get; set; }

        public ImageSource LargeIcon { get; set; }

        public ICommandEx MenuCommandParent { get; set; }

        public ICommandEx PopupCommandParent { get; set; }

        public int Weight { get; set; }

        public ShellKeyGesture KeyGuesture { get; set; }

        public void AttacheVisualElement()
        {
            throw new NotImplementedException();
        }

        public void DetachVisualElement()
        {
            throw new NotImplementedException();
        }

        public ICommandSourceEx CreateDefaultVisual(CommandDisplaySituation situation)
        {
            throw new NotImplementedException();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteHanlder(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }

        public void RaiseCanExecuteChanged()
        {

            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
