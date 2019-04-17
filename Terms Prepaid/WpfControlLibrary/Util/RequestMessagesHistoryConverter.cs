using System;
using System.Globalization;
using System.Windows.Data;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.Util
{
    public class RequestMessagesHistoryConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 3)
                return null;

            var msg = (RequestMessage) values[0];

            var msgStatus = msg == null 
                ? " " 
                : !msg.IsIncomming
                    ? (msg.Tracking != null
                        ? string.Format("Доставлено {0:HH:mm} / {0:dd.MM.yy}", (DateTime) msg.Tracking)
                        : (msg.Sent != null
                            ? string.Format("Отправлено {0:HH:mm} / {0:dd.MM.yy}", (DateTime) msg.Sent)
                            : "Ошибка отправки сообщения"))
                    : (msg.Seen && msg.ReadDate != null
                        ? string.Format("Последнее сообщение {0:HH:mm} / {0:dd.MM.yy}", (DateTime) msg.ReadDate)
                        : string.Format("Новое сообщение {0:HH:mm} / {0:dd.MM.yy}", msg.Date));

            var msgCountAll = (int)values[1];
            var msgCountNew = (int)values[2];

            var msgCount = string.Format("Сообщений: {0}, новых: {1}", msgCountAll, msgCountNew);

            return string.Format("{0}\n{1}", msgCount, msgStatus);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}