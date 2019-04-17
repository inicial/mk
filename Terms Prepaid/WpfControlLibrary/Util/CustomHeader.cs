using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfControlLibrary.Util
{
    public static class CustomHeader
    {
        public static int GetUnit(DependencyObject obj) { return (int)obj.GetValue(UnitProperty); }
        public static void SetUnit(DependencyObject obj, int value) { obj.SetValue(UnitProperty, value); }

        public static readonly DependencyProperty UnitProperty = DependencyProperty.RegisterAttached(
            "Unit", typeof(int), typeof(CustomHeader), new FrameworkPropertyMetadata(null));
    }
}
