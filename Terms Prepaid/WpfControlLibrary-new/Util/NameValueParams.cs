using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfControlLibrary.Util
{
    public static class NameValueParams
    {
        public static int GetWidth1(DependencyObject obj) { return (int)obj.GetValue(Width1Property); }

        public static void SetWidth1(DependencyObject obj, int value) { obj.SetValue(Width1Property, value); }

        public static readonly DependencyProperty Width1Property = DependencyProperty.RegisterAttached("Width1",
            typeof (int), typeof (NameValueParams),
            new FrameworkPropertyMetadata());


        public static int GetWidth2(DependencyObject obj) { return (int)obj.GetValue(Width2Property); }

        public static void SetWidth2(DependencyObject obj, int value) { obj.SetValue(Width2Property, value); }

        public static readonly DependencyProperty Width2Property = DependencyProperty.RegisterAttached("Width2",
            typeof(int), typeof(NameValueParams),
            new FrameworkPropertyMetadata());
    }
}
