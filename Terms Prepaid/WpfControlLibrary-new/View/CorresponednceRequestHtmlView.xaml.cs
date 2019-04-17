﻿using System;
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
using WpfControlLibrary.Helpers;
using WpfControlLibrary.ViewModel;
using ScrollHelper = WpfControlLibrary.Util.ScrollHelper;

namespace WpfControlLibrary.View
{
    /// <summary>
    /// Interaction logic for CorresponednceTabView2.xaml
    /// </summary>
    public partial class CorresponednceRequestHtmlView : UserControl
    {
        public CorresponednceRequestHtmlView()
        {
            InitializeComponent();
        }

        public void ScrollToBottom()
        {
            //var sv = ScrollHelper.FindScrollViewer(Browser);
            //if (sv != null) sv.ScrollToBottom();
            var html = Browser.Document as mshtml.HTMLDocument;
            if (html != null) html.parentWindow.scroll(100000, 100000);
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            ((ICorrespondenceRequestBaseViewModel)DataContext).ContentHtml = new HtmlWrapper().Wrap(Editor.ContentHtml);
            Editor.ContentHtml = "";
            ScrollToBottom();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //RichTextBox.Document = new FlowDocument();
        }

        private void Browser_OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri != null)
            {
                ((ICorrespondenceRequestBaseViewModel)DataContext).GetAttachment(e.Uri.ToString());
                e.Cancel = true;
            }
        }
    }
}
