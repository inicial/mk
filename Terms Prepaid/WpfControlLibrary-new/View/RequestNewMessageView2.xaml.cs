using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for RequestNewMessageView2.xaml
    /// </summary>
    public partial class RequestNewMessageView2 : SimpleWindow, IRequestNewMessageView
    {
        private static IRequestNewMessageView _instance;

        private RequestNewMessageView2()
        {
            InitializeComponent();
        }
        
        public static IRequestNewMessageView GetInstance()
        {
            return _instance ?? (_instance = new RequestNewMessageView2());
        }

        protected override void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;

            Editor.InnerHtml = string.Empty;
            //Editor.Clear();
            var vm = (ICorrespondenceRequestBaseViewModel)DataContext;
            vm.RemoveAttachments();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            ((ICorrespondenceRequestBaseViewModel)DataContext).ContentHtml = new HtmlWrapper().Wrap(Editor.DocumentHtml);
            //var vm = (ICorrespondenceRequestBaseViewModel) DataContext;
            //vm.ContentHtml = new HtmlWrapper().Wrap(Editor.Html);
            Close();
        }
    }
}
