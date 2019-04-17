using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
                        ? string.Format("��������� {0:HH:mm} / {0:dd.MM.yy}", (DateTime)msg.Tracking)
                        : (msg.Sent != null
                            ? string.Format("���������� {0:HH:mm} / {0:dd.MM.yy}", (DateTime)msg.Sent)
                            : "������ �������� ���������"))
                    : string.Format("�������� {0:HH:mm} / {0:dd.MM.yy}", msg.Date);
                        //(msg.ReadDate != null
                        //? string.Format("�������� {0:HH:mm} / {0:dd.MM.yy}", (DateTime)msg.ReadDate)
                        //: string.Format("�������� {0:HH:mm} / {0:dd.MM.yy}", msg.Date));

            if (msg == null)
                return "��������� ���";

            var from = msg.Mod == RequestMessageMod.MTC ? "������" : "���������";
            var prefix = msg.IsIncomming ? " �� " : " ";
            var postfix = msg.IsIncomming ? "�" : "�";

            var msgCount = string.Format("���������{1}{0}{2}", from, prefix, postfix);

            return string.Format("{0}\n{1}", msgCount, msgStatus);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RequestMessagesHistoryConverter3 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var msg = (RequestMessageInfo)value;

            if (msg == null)
                return "��������� ���";
            
            var sb = new StringBuilder();
            var byWhom = msg.Mod == RequestMessageMod.MTC ? "������" : "���������";

            sb.Append(msg.IsIncomming ? string.Format("����� �� {0}�", byWhom) : string.Format("��������� {0}�", byWhom));

            sb.Append("\n");

            sb.Append(!msg.IsIncomming 
                ? msg.Sent != null
                    ? string.Format("���������� {0:dd.MM.yy} / {0:HH:mm}", (DateTime)msg.Sent) 
                    : "������ �������� ���������"
                : string.Format("������� {0:dd.MM.yy} / {0:HH:mm}", msg.Date));

            if (msg.Tracking != null)
                sb.Append(string.Format("\n����������� {0:dd.MM.yy} / {0:HH:mm}", (DateTime)msg.Tracking, byWhom));

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}