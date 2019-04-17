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
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Buttons
{
    /// <summary>
    /// Interaction logic for ArrivalsButton.xaml
    /// </summary>
    public partial class ArrivalsButton : UserControl
    {
        public event MyControlEventHandlerSample OnButtonClick;

        public ArrivalsButton()
        {
            InitializeComponent();
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

        /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnOption.IsChecked = false;
            if (OnButtonClick != null)
                OnButtonClick(this);
        }
         * */
    }
}
