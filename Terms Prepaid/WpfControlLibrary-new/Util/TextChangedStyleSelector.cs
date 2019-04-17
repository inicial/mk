using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class TextChangedStyleSelector : StyleSelector
    {
        public Style DefaultStyle { get; set; }
        public Style ChangedStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is bool)
                return (bool) item ? ChangedStyle : DefaultStyle;

            return base.SelectStyle(item, container);
        }
    }

    public class TextStyleSelector : IValueConverter
    {
        public Style DefaultStyle { get; set; }
        public Style ChangedStyle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return (bool)value ? ChangedStyle : DefaultStyle;

            return DefaultStyle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
