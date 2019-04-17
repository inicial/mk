using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary.Helpers;
using UserControl = System.Windows.Controls.UserControl;
using mshtml;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for CkEditorView.xaml
    /// </summary>
    public partial class CkEditorView : UserControl
    {
        private readonly SkEditorHelper _skEditorHelper;

        public HTMLDocument Document 
        { 
            get { return Browser.Document.DomDocument as HTMLDocument; }
        }

        public string Html
        {
            get { return _skEditorHelper.GetContent(Document); }
        }

        public CkEditorView()
        {
            InitializeComponent();
            _skEditorHelper = new SkEditorHelper();

            var curDir = Directory.GetCurrentDirectory();
            Browser.Url = new Uri(String.Format("file:///{0}/CkEditor.html", curDir));
        }

        private void Browser_OnNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!e.Url.IsFile || Browser.Document == null) return;

            e.Cancel = true;
            var imgTag = FileHelper.GetImageTagFromFile(e.Url.LocalPath);
            _skEditorHelper.InsertHtml(Browser.Document.DomDocument as HTMLDocument, imgTag);
        }

        private void Browser_OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var wb = sender as WebBrowser;
            DisableScriptErrors(wb, true);
        }

        public void DisableScriptErrors(WebBrowser wb, bool hide)
        {
            var fiComWebBrowser = typeof(WebBrowser).GetField("activeXInstance", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            var objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }

        public void InsertImage()
        {
            if (Browser.Document != null)
            {
                var doc = Browser.Document.DomDocument as HTMLDocument;
                var filePath = FileHelper.OpenFileGetFilePath();
                var imgTag = FileHelper.GetImageTagFromFile(filePath);
                _skEditorHelper.InsertHtml(doc, imgTag);
            }
        }

        public void Clear()
        {
            _skEditorHelper.SetContent(Browser.Document.DomDocument as HTMLDocument, null);
        }
    }
}
