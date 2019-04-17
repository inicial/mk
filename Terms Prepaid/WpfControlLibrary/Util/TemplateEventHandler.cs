using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfControlLibrary.Util
{
    public static class TemplateEventHandler
    {
        public delegate void OnClickHandlerDelegate(object sender, RoutedEventArgs e);

        public static readonly DependencyProperty OnClickHandlerProperty = DependencyProperty.RegisterAttached(
        "OnClickHandler", typeof(bool), typeof(TemplateEventHandler), new PropertyMetadata(false));

        public static void SetOnClickHandler(DependencyObject element, OnClickHandlerDelegate value)
        {
            element.SetValue(OnClickHandlerProperty, value);
        }

        public static OnClickHandlerDelegate GetOnClickHandler(DependencyObject element)
        {
            return (OnClickHandlerDelegate)element.GetValue(OnClickHandlerProperty);
        }
    }
}
