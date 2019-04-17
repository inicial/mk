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
using WpfControlLibrary.Util;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for CorresponednceTabView2.xaml
    /// </summary>
    public partial class CorresponednceTabView2 : UserControl
    {
        public CorresponednceTabView2()
        {
            InitializeComponent();
        }

        /*private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RichTextBox.Document = ((ICorrespondenceBaseViewModel) DataContext).Document;
        }*/
        
        public void ScrollToBottom()
        {
            var sv = ScrollHelper.FindScrollViewer(Viewer);
            if (sv != null) sv.ScrollToBottom();
        }
         
        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            ScrollToBottom();
        }

    }
}
