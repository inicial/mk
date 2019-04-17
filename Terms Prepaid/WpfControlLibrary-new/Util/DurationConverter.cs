using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class DurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = value as TimeSpan? ?? new TimeSpan();

            var sb = new StringBuilder();

            if (d.Days > 0) sb.AppendFormat("{0} дн", d.Days);
            if (d.Hours > 0) sb.AppendFormat(" {0} час", d.Hours);
            if (d.Minutes > 0) sb.AppendFormat(" {0} мин", d.Minutes);

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
