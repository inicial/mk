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
    /// Interaction logic for RequestMessageView.xaml
    /// </summary>
    public partial class RequestMessageView : UserControl
    {
        public RequestMessageView()
        {
            InitializeComponent();
        }

        private void Browser_OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri != null)
            {
                System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
                e.Cancel = true;
            }
        }
    }
}
