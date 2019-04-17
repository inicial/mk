using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class VoucherViewGroupStatusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string status = values[0].ToString();
            DateTime? date = Parser.GetDateTime(values[1]);

            var sb = new StringBuilder(status);

            if (date != null)
            {
                sb.Append(Environment.NewLine);
                sb.Append(date);
            }

            return sb.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
