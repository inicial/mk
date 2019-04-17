using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Buttons
{
    public class SimpleWpfButton : UserControl, IButton
    {
        public event MyControlEventHandlerSample OnButtonClick;

        protected void Invoke(object obj)
        {
            if (OnButtonClick != null)
                OnButtonClick.Invoke(obj);
        }

        protected virtual void Button_Click(object sender, RoutedEventArgs e)
        {
            Invoke(this);
        }
    }
}
