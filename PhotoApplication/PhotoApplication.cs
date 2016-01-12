using SmartFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhotoApplication
{    
    [Export(typeof(ShellApplication))]
    public class PhotoApplication : ShellApplication
    {
        // public static readonly RoutedCommand SearchCommand = new RoutedCommand();
        // public void InitializeCommandBinding()
        // {
        //     var commandBinding = new CommandBinding();
        //     commandBinding.Command = SearchCommand;
        //     SearchCommand.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
        //     commandBinding.CanExecute += commandBinding_CanExecute;
        //     commandBinding.Executed += commandBinding_Executed;
        // 
        //     MainWindow.CommandBindings.Add(commandBinding);
        // }

        // void commandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        // {
        // }
        // 
        // void commandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        // {
        //     e.CanExecute = true;
        //     e.Handled = true;
        // }
        [STAThread]
        public static void Main()
        {
            var photoApplication = new PhotoApplication();
            photoApplication.Run();
        }
    }
}
