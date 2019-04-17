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
using System.Windows.Shapes;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for ChangeSenderAddressView.xaml
    /// </summary>
    public partial class ChangeSenderAddressView : SimpleWindow, IChangeSenderAddressView
    {
        public ChangeSenderAddressView()
        {
            InitializeComponent();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
