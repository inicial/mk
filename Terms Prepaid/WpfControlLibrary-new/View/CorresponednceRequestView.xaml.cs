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
    public partial class CorresponednceRequestView : UserControl
    {
        public CorresponednceRequestView()
        {
            InitializeComponent();
        }

        public void ScrollToBottom()
        {
            var sv = ScrollHelper.FindScrollViewer(Viewer);
            if (sv != null) sv.ScrollToBottom();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            ((ICorrespondenceRequestBaseViewModel)DataContext).Document = FlowDoc;
            ScrollToBottom();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //RichTextBox.Document = new FlowDocument();
        }

        private void FlowDocument_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("ImageSource"))
            {
                ImageSource img = (ImageSource)e.Data.GetData("ImageSource");

                (sender as FlowDocument).Blocks.Add(new BlockUIContainer(new Image() { Source = img }));
            }
        }
    }
}
