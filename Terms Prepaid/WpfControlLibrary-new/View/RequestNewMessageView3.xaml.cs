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
    /// Interaction logic for RequestNewMessageView2.xaml
    /// </summary>
    public partial class RequestNewMessageView3 : SimpleWindow, IRequestNewMessageView
    {
        private static IRequestNewMessageView _instance;
        private readonly SkEditorHelper _skEditorHelper;

        private RequestNewMessageView3()
        {
            InitializeComponent();
            _skEditorHelper = new SkEditorHelper();
            //LoadCkEditor(WebEditor);
            //Browser.Url = new Uri("http://yandex.ru/");
            Browser.Url = new Uri("http://shche.pe.hu/");
        }

        public static IRequestNewMessageView GetInstance()
        {
            return _instance ?? (_instance = new RequestNewMessageView3());
        }

        protected override void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;

            //_skEditorHelper.SetContent(WebEditor.Document as mshtml.HTMLDocument, null);
            _skEditorHelper.SetContent(Browser.Document.DomDocument as mshtml.HTMLDocument, null);
            var vm = (ICorrespondenceRequestBaseViewModel)DataContext;
            vm.RemoveAttachments();
        }

        private void Send(HTMLDocument doc)
        {
            var html = _skEditorHelper.GetContent(doc);
            ((ICorrespondenceRequestBaseViewModel)DataContext).ContentHtml = new HtmlWrapper().Wrap(html);
            _skEditorHelper.SetContent(doc, null);
        }

        private void InsertHtml(HTMLDocument doc)
        {
            var filePath = FileHelper.OpenFileGetFilePath();
            var imgTag = FileHelper.GetImageTagFromFile(filePath);
            //var imgTag = "<Img src=\"file:///C://Users/s.chekotkov/Desktop/1407821858_511858608.jpg\"/>";
            _skEditorHelper.InsertHtml(doc, imgTag);
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (!((IRequestNewMessageViewModelBase) DataContext).ValidateSenderAddress())
                return;
            
            Send(Browser.Document.DomDocument as HTMLDocument);
            Close();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //InsertHtml(WebEditor.Document as HTMLDocument);
            InsertHtml(Browser.Document.DomDocument as HTMLDocument);
        }

        private void LoadCkEditor(WebBrowser browser)
        {
            if (browser == null)
                return;

            string curDir = Directory.GetCurrentDirectory();
            var uri = new Uri(String.Format("file:///{0}/CkEditor.html", curDir));
            browser.Source = new Uri("http://shche.pe.hu/");
        }

        private void WebEditor_OnLoaded(object sender, RoutedEventArgs e)
        {
            var browser = sender as WebBrowser;
        }

        /// <summary>
        /// Wpf control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebEditor_OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (!e.Uri.IsFile) return;

            e.Cancel = true;
            var imgTag = FileHelper.GetImageTagWithBase64FromFile(e.Uri.LocalPath);
            _skEditorHelper.InsertHtml(WebEditor.Document as HTMLDocument, imgTag);
        }

        /// <summary>
        /// WinForms control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Browser_OnNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!e.Url.IsFile) return;

            e.Cancel = true;
            var imgTag = FileHelper.GetImageTagFromFile(e.Url.LocalPath);
            _skEditorHelper.InsertHtml(Browser.Document.DomDocument as HTMLDocument, imgTag);
        }

        public void HideScriptErrors(System.Windows.Forms.WebBrowser wb, bool hide)
        {
            var fiComWebBrowser = typeof(System.Windows.Forms.WebBrowser).GetField("activeXInstance", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            var objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }

        private void Browser_OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var wb = sender as System.Windows.Forms.WebBrowser;
            HideScriptErrors(wb, true);
        }
    }
}
