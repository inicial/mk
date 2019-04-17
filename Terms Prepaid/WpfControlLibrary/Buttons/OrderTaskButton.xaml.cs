using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using WpfControlLibrary.Common;


namespace WpfControlLibrary
{
    public partial class OrderTaskButton : UserControl, IButton
    {
        public event MyControlEventHandlerSample OnButtonClick;

        public OrderTaskButton()
        {
            InitializeComponent();
        }

        private void popupOption_Closed(object sender, EventArgs e)
        {
            btnOption.IsChecked = false;
        }

        private void btnOption_Checked(object sender, RoutedEventArgs e)
        {
            if (OnButtonClick != null)
                OnButtonClick(this);
            btnOption.IsChecked = false;
            return;

            //popupOption.IsOpen = true;
            //popupOption.Closed -= popupOption_Closed;
            //popupOption.Closed += popupOption_Closed;
        }

        private void btnOption_Unchecked(object sender, RoutedEventArgs e)
        {
            //popupOption.IsOpen = false;
        }

        /*
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
        */
    }
}
