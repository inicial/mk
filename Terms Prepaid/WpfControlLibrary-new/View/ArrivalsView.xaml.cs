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

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for ArrivalsView.xaml
    /// </summary>
    public partial class ArrivalsView : UserControl
    {
        public ArrivalsView()
        {
            InitializeComponent();
        }

        /*
        private void popupOption_Closed(object sender, EventArgs e)
        {
            popupOption.IsChecked = false;
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
        }*/
    }
}
