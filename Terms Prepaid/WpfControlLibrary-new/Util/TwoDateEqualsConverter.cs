using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class TwoDateEqualsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? dt1 = Parser.GetDateTime(values[0]);
            DateTime? dt2 = Parser.GetDateTime(values[1]);

            if ((dt1 != null && dt2 != null && ((DateTime) dt1).Date.Equals(((DateTime) dt2).Date)) ||
                (dt1 == null && dt2 == null))
                return true;
            
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
