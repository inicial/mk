using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class RequestStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ObservableCollection<RequestStatus>)) return null;

            var col = value as ObservableCollection<RequestStatus>;

            var sb = new StringBuilder();

            foreach (var status in col.Where(s => s.Id != 0))
            {
                sb.Append(string.Format("{0} в {1:HH:mm} / {1:dd.MM.yy}", status.Name, status.Date));
                sb.Append("\n");
            }
            
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BindButtonEnabledConverter : IValueConverter
    {
        public static bool GetBindPermission(object value)
        {
            return value is Request;// && ((Request)value).Performer.Equals("Не назначен");
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetBindPermission(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class UserMailAdressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var user = value as User;
            if (user == null)
                return null;

            return string.Format("{0} <{1}>", user.Name, user.Email);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
