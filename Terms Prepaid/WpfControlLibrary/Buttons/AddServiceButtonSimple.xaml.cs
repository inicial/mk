using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary.Common;


namespace WpfControlLibrary
{
    public class MyControlEventArgs
    {
        public bool? Result;
        public string Text1;
        public string Text2;

        public MyControlEventArgs(string text1, string text2, bool? result = false)
        {
            Result = result;
            Text1 = text1;
            Text2 = text2;
        }
    }

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AddServiceButtonSimple : UserControl, IButton
    {
        //public delegate void MyControlEventHandler(object sender, MyControlEventArgs args);
        //public event MyControlEventHandler OnButtonClick;

        public event MyControlEventHandlerSample OnButtonClick;

        public int Selected = 0;

        public AddServiceButtonSimple(bool HasTransfer, bool HasInsurance)
        {
            InitializeComponent();

            if (!HasTransfer)
            {
                MenuItemTransfer.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (!HasInsurance)
            {
                MenuItemInsurance.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void popupOption_Closed(object sender, EventArgs e)
        {
            btnOption.IsChecked = false;
        }

        private void btnOption_Checked(object sender, RoutedEventArgs e)
        {
            popupOption.IsOpen = true;
            popupOption.Closed -= popupOption_Closed;
            popupOption.Closed += popupOption_Closed;
        }

        private void btnOption_Unchecked(object sender, RoutedEventArgs e)
        {
            popupOption.IsOpen = false;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            btnOption.IsChecked = false;
            Selected = 1;
            if (OnButtonClick != null)
                OnButtonClick(this);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            btnOption.IsChecked = false;
            Selected = 2;
            if (OnButtonClick != null)
                OnButtonClick(this);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            btnOption.IsChecked = false;
            Selected = 3;
            if (OnButtonClick != null)
                OnButtonClick(this);
        }
    }
}
