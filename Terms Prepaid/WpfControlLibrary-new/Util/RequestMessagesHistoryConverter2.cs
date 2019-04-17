using System;
using System.Globalization;
using System.Windows.Data;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.Util
{
    public class RequestMessagesHistoryConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var msg = (RequestMessageInfo)value;

            var msgStatus = msg == null
                ? " "
                : !msg.IsIncomming
                    ? (msg.Tracking != null
                        ? string.Format("Прочитано {0:HH:mm} / {0:dd.MM.yy}", (DateTime)msg.Tracking)
                        : (msg.Sent != null
                            ? string.Format("Отправлено {0:HH:mm} / {0:dd.MM.yy}", (DateTime)msg.Sent)
                            : "Ошибка отправки сообщения"))
                    : string.Format("Получено {0:HH:mm} / {0:dd.MM.yy}", msg.Date);
                        //(msg.ReadDate != null
                        //? string.Format("Получено {0:HH:mm} / {0:dd.MM.yy}", (DateTime)msg.ReadDate)
                        //: string.Format("Получено {0:HH:mm} / {0:dd.MM.yy}", msg.Date));

            if (msg == null)
                return "Сообщений нет";

            var from = msg.Mod == RequestMessageMod.MTC ? "клиент" : "сотрудник";
            var prefix = msg.IsIncomming ? " от " : " ";
            var postfix = msg.IsIncomming ? "а" : "у";

            var msgCount = string.Format("Сообщение{1}{0}{2}", from, prefix, postfix);

            return string.Format("{0}\n{1}", msgCount, msgStatus);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}