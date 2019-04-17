using System;
using System.Globalization;
using System.Windows.Data;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.Util
{
    public class RequestMessagesSeenStatusConvertor5 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is RequestMessage))
                return null;

            var msg = (RequestMessage)value;

            return !msg.IsIncomming && msg.Tracking != null
                ? string.Format("Прочитано {0:HH:mm} / {0:dd.MM.yy}", (DateTime) msg.Tracking)
                : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}