using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Gecko.WebIDL;

namespace WpfControlLibrary.Common
{
    public interface IMessageBoxService
    {
        bool ShowMessage(string text, string caption, MessageBoxButton button);
        bool ShowMessage(string text, string caption, MessageBoxButton button, MessageBoxImage icon);
    }

    public class MessageBoxService : IMessageBoxService
    {
        public bool ShowMessage(string text, string caption, MessageBoxButton button)
        {
            var result = MessageBox.Show(text, caption, button);
            return result == MessageBoxResult.OK || result == MessageBoxResult.Yes;
        }

        public bool ShowMessage(string text, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            var result = MessageBox.Show(text, caption, button, icon);
            return result == MessageBoxResult.OK || result == MessageBoxResult.Yes;
        }
    }
}
