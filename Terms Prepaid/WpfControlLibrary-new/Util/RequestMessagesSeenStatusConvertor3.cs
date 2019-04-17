using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class RequestMessagesSeenStatusConvertor3 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && (bool)value ? "Просмотрена" : "Не просмотрена"; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}