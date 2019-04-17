using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.ViewModel;
using mshtml;
using DragEventArgs = System.Windows.DragEventArgs;
using WebBrowser = System.Windows.Controls.WebBrowser;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for RequestNewMessageView3.xaml
    /// </summary>
    public partial class RequestNewMessageView : SimpleWindow, IRequestNewMessageView
    {
        private static IRequestNewMessageView _instance;

        private RequestNewMessageView()
        {
            InitializeComponent();
        }

        public static IRequestNewMessageView GetInstance()
        {
            return _instance ?? (_instance = new RequestNewMessageView());
        }

        protected override void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;

            Editor.Clear();
            var vm = (ICorrespondenceRequestBaseViewModel)DataContext;
            vm.RemoveAttachments();
        }

        private void SendBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (!((IRequestNewMessageViewModelBase) DataContext).ValidateSenderAddress())
                return;

            ((ICorrespondenceRequestBaseViewModel)DataContext).ContentHtml = new HtmlWrapper().Wrap(Editor.Html);
            Editor.Clear();
            Close();
        }

        private void InsertImageBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Editor.InsertImage();
        }
    }
}
