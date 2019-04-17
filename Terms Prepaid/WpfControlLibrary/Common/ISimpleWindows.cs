using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms.Integration;

namespace WpfControlLibrary.Common
{
    public interface ISimpleWindows
    {
        object DataContext { get; set; }
        void Show();
        bool? ShowDialog();
        void EnableModelessKeyboardInterop();
    }

    public class SimpleWindow : Window
    {
        private bool _modelessKeyboardInteropEnabled;

        public SimpleWindow()
        {
            Closing += Window_Closing;
        }

        protected virtual void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        public void EnableModelessKeyboardInterop()
        {
            if (_modelessKeyboardInteropEnabled) return;
            ElementHost.EnableModelessKeyboardInterop(this);
            _modelessKeyboardInteropEnabled = true;
        }
    }

    public interface ISelectManagerView : ISimpleWindows
    {

    }

    public interface IRequestNewMessageView : ISimpleWindows
    {
         
    }

    public interface ISelectCancelationReasonView : ISimpleWindows
    {

    }

    public interface IChangeSenderAddressView : ISimpleWindows
    {

    }

    public interface ITestView : ISimpleWindows
    {

    }

    public interface ICallRecordsView : ISimpleWindows
    {

    }

    public interface IProblemRequestsView : ISimpleWindows
    {

    }
}
