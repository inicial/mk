using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class RequestMessagesSeenStatusConvertor2 : IMultiValueConverter
    {
        public static string GetSeenStatus(object[] values)
        {
            return values != null && values.Length == 2 && values[0] is int && values[1] is int
                ? ((int) values[0] + (int) values[1] == 0 ? "Просмотрена" : "Не просмотрена")
                : "";
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return GetSeenStatus(values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}