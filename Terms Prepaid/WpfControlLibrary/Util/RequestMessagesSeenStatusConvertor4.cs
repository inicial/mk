using System;
using System.Globalization;
using System.Windows.Data;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.Util
{
    public class RequestMessagesSeenStatusConvertor4 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is RequestMessage))
                return null;
            
            var msg = (RequestMessage) value;

            if (!msg.IsIncomming)
            {
                if (msg.Tracking != null)
                    return string.Format("Доставлено {0:HH:mm} / {0:dd.MM.yy}", (DateTime) msg.Tracking);

                if (msg.Sent != null)
                    return string.Format("Отправлено {0:HH:mm} / {0:dd.MM.yy}", (DateTime) msg.Sent);
                
                return "Ошибка отправки сообщения";
            }
            else
            {
                if (msg.Seen && msg.ReadDate != null)
                    return string.Format("Последнее сообщение {0:HH:mm} / {0:dd.MM.yy}", (DateTime)msg.ReadDate);
                
                return string.Format("Новое сообщение {0:HH:mm} / {0:dd.MM.yy}", msg.Date);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}