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

namespace WpfControlLibrary
{
    /// <summary>
    /// Interaction logic for AweWebControl.xaml
    /// </summary>
    public partial class AweWebControl : UserControl
    {
        public AweWebControl()
        {
            InitializeComponent();
        }

        public void GoToUrl(Uri uri)
        {
            Url.Text = uri.ToString();
            Broser.Navigate(uri);
            //webControl.Source = uri;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Broser.Navigate(new Uri(Url.Text));
        }
    }
}
