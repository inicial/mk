using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class GridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value * 3 / 11;
            GridLength gridLength = new GridLength(val);

            return gridLength;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GridLength val = (GridLength)value;

            return val.Value;
        }
    }

    public class GridLengthConverter2 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 4)
                return null;

            if (values.Any(val => !(val is double))) return null;

            var fullLength = (double)values[0];
            var actualLenght = (double)values[1];
            var numerator = (double)values[2];
            var denominator = (double)values[3];

            var length = fullLength * numerator / denominator;

            GridLength gridLength = new GridLength(Math.Min(length, actualLenght));
            return gridLength;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
