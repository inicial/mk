using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WpfControlLibrary.Common;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WpfControlLibrary.Helpers
{
    public interface IWindowsHelper
    {
        ISimpleWindows GetWindow(object vm, ISimpleWindows win);
        void ShowWindow(object vm, ISimpleWindows win, bool enableModelessKeyboardInterop = true, bool modal = false);
        void ShowMessage(string msg, string title);
    }

    public class WindowsHelper : IWindowsHelper
    {
        private static WindowsHelper _instance;

        private WindowsHelper()
        {
            
        }

        public static WindowsHelper GetInstance()
        {
            return _instance ?? (_instance = new WindowsHelper());
        }

        public ISimpleWindows GetWindow(object vm, ISimpleWindows win)
        {
            win.DataContext = vm;
            return win;
        }

        public void ShowWindow(object vm, ISimpleWindows win, bool enableModelessKeyboardInterop = true, bool modal = false)
        {
            win.DataContext = vm;
            var window = win as Window;
            if (window != null) win.EnableModelessKeyboardInterop();
            if (modal)
                win.ShowDialog();
            else
                win.Show();
        }

        public void ShowMessage(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
