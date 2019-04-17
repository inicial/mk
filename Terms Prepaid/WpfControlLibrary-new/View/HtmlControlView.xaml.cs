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
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for HtmlControlView.xaml
    /// </summary>
    public partial class HtmlControlView : UserControl
    {
        public HtmlControlView()
        {
            InitializeComponent();
            //Xpcom.Initialize("Firefox");
        }

        private void WebBrowser_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;
            
            //string script = "document.body.style.overflow ='hidden'";
            //wb.InvokeScript("execScript", new Object[] { script, "JavaScript" });
            Grid.Height = (int)(wb.Document as dynamic).body.scrollHeight + 20;
        }

        private void HtmlChanged()
        {
            /*var vm = DataContext as IHtmlControlViewModel;
            if (vm != null)
                Editor.ContentHtml = vm.HtmlToDisplay;
                //browser.LoadHtml("<html><body><h1>Hello!!!</h1></body></html>"); //.LoadHtml(vm.HtmlToDisplay);*/
        }

        private void AttachHandler()
        {
            /*var vm = DataContext as IHtmlControlViewModel;
            if (vm != null) vm.HtmlChangedHandler = HtmlChanged;*/
        }

        private void HtmlControlView_OnLoaded(object sender, RoutedEventArgs e)
        {
            //AttachHandler();
        }
    }
}
