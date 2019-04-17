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
    /// Interaction logic for ShowRequestMessageButton.xaml
    /// </summary>
    public partial class RequestMessageButton : UserControl, IRequestMessageButton
    {
        public RequestMessageButton()
        {
            InitializeComponent();
        }

        /*private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            Popup.IsOpen = !Popup.IsOpen;
        }*/
    }
}
