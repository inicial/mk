using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfControlLibrary.Util
{
    public static class ScrollHelper
    {
        public static ScrollViewer FindScrollViewer(FlowDocumentScrollViewer flowDocumentScrollViewer)
        {
            if (VisualTreeHelper.GetChildrenCount(flowDocumentScrollViewer) == 0)
                return null;

            DependencyObject firstChild = VisualTreeHelper.GetChild(flowDocumentScrollViewer, 0);
            if (firstChild == null)
                return null;

            Decorator border = VisualTreeHelper.GetChild(firstChild, 0) as Decorator;

            if (border == null)
                return null;

            return border.Child as ScrollViewer;
        }

        public static ScrollViewer FindScrollViewer(WebBrowser browser)
        {
            if (VisualTreeHelper.GetChildrenCount(browser) == 0)
                return null;

            DependencyObject firstChild = VisualTreeHelper.GetChild(browser, 0);
            if (firstChild == null)
                return null;

            Decorator border = VisualTreeHelper.GetChild(firstChild, 0) as Decorator;

            if (border == null)
                return null;

            return border.Child as ScrollViewer;
        }
    }
}
