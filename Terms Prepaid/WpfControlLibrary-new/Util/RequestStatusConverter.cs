using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.Util
{
    public class RequestStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as RequestStatus;
            if (val == null) return null;

            string date = val.Date != DateTime.MinValue ? string.Format("{0:HH:mm} / {0:dd.MM.yy}", val.Date) : "";

            return /*val.Id == 6 ? null : */ string.Format("{0} {1}", val.Name, date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
