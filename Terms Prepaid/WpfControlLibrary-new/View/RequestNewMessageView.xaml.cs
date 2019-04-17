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
using Smith.WPF.HtmlEditor;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for RequestNewMessageView.xaml
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
            Editor.ContentHtml = "";
            //Editor.Clear();
            var vm = (ICorrespondenceRequestBaseViewModel)DataContext;
            vm.RemoveAttachments();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            ((ICorrespondenceRequestBaseViewModel)DataContext).ContentHtml = new HtmlWrapper().Wrap(Editor.ContentHtml);
            //var vm = (ICorrespondenceRequestBaseViewModel) DataContext;
            //vm.ContentHtml = new HtmlWrapper().Wrap(Editor.Html);
            Close();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var content = String.Format( "<!DOCTYPE html><html lang=\"ru\"><head><meta charset=\"utf-8\"></head><body>{0}</body></html>", Editor.ContentHtml);
            Editor.ContentHtml = content;
        }
    }
}
