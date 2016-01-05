using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileBrowser.View;

namespace FileBrowser.ViewModel
{
    public class FileBrowserHistory
    {
        private static Stack<string> BackHistory = new Stack<string>();
        private static Stack<string> ForwardHistory = new Stack<string>();

        public static void PushBackHistory(string history)
        {
            if (BackHistoryCount() != 0 && history == GetTopBackHistory())
            {
                return;
            }
            BackHistory.Push(history);
            FileItemsView.BackCommand.RaiseCanExecuteChanged();
        }

        public static string PopBackHistory()
        {
            var history = BackHistory.Pop();
            FileItemsView.BackCommand.RaiseCanExecuteChanged();
            return history;
        }

        public static string GetTopBackHistory()
        {
            return BackHistory.Peek();
        }

        public static int BackHistoryCount()
        {
            return BackHistory.Count;
        }

        public static void PushForwardHistory(string history)
        {
            if (ForwardHistoryCount() != 0 && history == GetTopForwardHistory())
            {
                return;
            }
            ForwardHistory.Push(history);
            FileItemsView.ForwardCommand.RaiseCanExecuteChanged();
        }

        public static string PopForwardHistory()
        {
            var history = ForwardHistory.Pop();
            FileItemsView.ForwardCommand.RaiseCanExecuteChanged();
            return history;
        }

        public static string GetTopForwardHistory()
        {
            return ForwardHistory.Peek();
        }

        public static int ForwardHistoryCount()
        {
            return ForwardHistory.Count;
        }
    }
}
