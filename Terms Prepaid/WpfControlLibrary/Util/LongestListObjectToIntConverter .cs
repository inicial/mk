using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfControlLibrary.Util
{
    public abstract class LongestListObjectToIntConverterBase
    {
        public int MinWidth { get; set; }
        public int Offset { get; set; }

        protected LongestListObjectToIntConverterBase()
        {
            MinWidth = 100;
            Offset = 25;
        }

        protected object GetWidth(IEnumerable<KeyValuePair<int, string>> pairs, Control control)
        {
            return pairs == null ? MinWidth : pairs.Max(bar => MeasureString(bar.Value, control).Width + Offset);
        }

        private Size MeasureString(string candidate, Control control)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch), control.FontSize, Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
        }
    }
    
    public class LongestListObjectToIntConverter : LongestListObjectToIntConverterBase, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pairs = value as IEnumerable<KeyValuePair<int, string>>;
            var c = new Control();
            return GetWidth(pairs, c);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class LongestListObjectToIntConverter2 : LongestListObjectToIntConverterBase, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var pairs = values[0] as IEnumerable<KeyValuePair<int, string>>;
            var comboBox = values[1] as Control;

            return GetWidth(pairs, comboBox);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
