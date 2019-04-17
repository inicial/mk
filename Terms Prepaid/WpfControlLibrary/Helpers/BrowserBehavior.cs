using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfControlLibrary.Helpers
{
    public static class BrowserBehavior
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
            "Html",
            typeof(string),
            typeof(BrowserBehavior),
            new FrameworkPropertyMetadata(OnHtmlChanged));

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetHtml(WebBrowser d)
        {
            return (string)d.GetValue(HtmlProperty);
        }

        public static void SetHtml(WebBrowser d, string value)
        {
            d.SetValue(HtmlProperty, value);
        }

        static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser wb = d as WebBrowser;
            if (wb == null) return;

            if (e.NewValue != null)
                wb.NavigateToString(e.NewValue as string);

            var html = wb.Document as mshtml.HTMLDocument;
            
            if (html != null) 
                html.parentWindow.scroll(100000, 100000);
        }
    }

    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty BodyProperty =
        DependencyProperty.RegisterAttached("Body", typeof(string), typeof(WebBrowserUtility), new PropertyMetadata(OnBodyChanged));

        public static string GetBody(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(BodyProperty);
        }

        public static void SetBody(DependencyObject dependencyObject, string body)
        {
            dependencyObject.SetValue(BodyProperty, body);
        }

        private static void OnBodyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var webBrowser = d as WebBrowser;
            if (!string.IsNullOrWhiteSpace(e.NewValue as string) && webBrowser != null)
            {
                if (Application.Current.MainWindow != null && !DesignerProperties.GetIsInDesignMode(Application.Current.MainWindow))
                {
                    webBrowser.NavigateToString((string)e.NewValue);
                    var html = webBrowser.Document as mshtml.HTMLDocument;
                    if (html != null) html.parentWindow.scroll(1000000, 20);
                }
            }
        }

    }
}
