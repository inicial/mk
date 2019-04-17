using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class ProcentAndNumericValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            decimal? procent = Parser.GetDecimal(values[0]);
            decimal? numeric = Parser.GetDecimal(values[1]);
            string rate = values[2] as string;

            StringBuilder sb = new StringBuilder();

            if (procent != null)
            {
                sb.Append((decimal) procent);
                
                if(procent != 0)
                    sb.Append(" %");
            }

            if (numeric != null && rate != null)
            {
                if (numeric != 0)
                {
                    sb.Append(string.Format(" ({0} {1})", ((decimal)numeric).ToString("F2").Replace(',','.'), rate));
                }
            }
            return sb.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
