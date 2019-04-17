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
using Gecko;

namespace WpfControlLibrary
{
    /// <summary>
    /// Interaction logic for GeckoWebControl.xaml
    /// </summary>
    public partial class GeckoWebControl2 : UserControl
    {
        public GeckoWebControl2()
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");
        }

        private void Btn_OnClick(object sender, RoutedEventArgs e)
        {
            //browser.Navigate("http://google.ru");
        }
    }
}
