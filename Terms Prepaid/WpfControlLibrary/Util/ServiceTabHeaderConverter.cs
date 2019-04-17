using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class ServiceTabHeaderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string name = values[0] as string;
            string number = values[1] as string;
            string dateBegin = values[2] as string;
            string dateEnd = values[3] as string;

            return String.Format("{0}{1}  {2}-{3}", name, number, dateBegin, dateEnd);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
