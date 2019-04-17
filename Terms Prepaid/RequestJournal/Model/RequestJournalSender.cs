using System;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    ///  Отправка писем с помощью Smtp
    /// </summary>
    public class RequestJournalSender : IRequestJournalSender
    {
        public IMailConfig Config { get; protected set; }
        private string _nullPixelUrl = "http://shche.pe.hu/getimage.php?eid";

        public string _googleAnalyticsPrefix =
            "http://www.google-analytics.com/collect?v=1&tid=UA-88038841-1&cid=*|UNIQID|*&t=event&ec=email&ea=open&el=*|UNIQID|*&cs=newsletter&cm=email&cn=";

        public string _googleAnalyticsPostfix = "&cm1=1";

        public RequestJournalSender(IMailConfig config)
        {
            Config = config;
        }

        public DateTime SendMessage(RequestMessage message, bool isHtml, bool deliveryNotification)
        {
            using (var client = new SmtpClient(Config.Host))
            {
                return isHtml ? SendHtmlMessage(client, message, deliveryNotification) : Send(client, message);
            }
        }

        private DateTime Send(SmtpClient client, RequestMessage msg)
        {
            client.Send(msg.SenderAddress, msg.DestinationAddress, msg.Theme, msg.Text);
            return DateTime.Now;
        }

        private DateTime SendHtmlMessage(SmtpClient client, RequestMessage msg, bool deliveryNotification)
        {
            var htmlWithNullPixel = AppendNullPixel(msg.Html, msg.Id);
            var htmlWithGoogleAnalytics = AppendGoogleAnalytics(htmlWithNullPixel, msg.Id);
            var message = new MailMessage(msg.SenderAddress, msg.DestinationAddress, msg.Theme, htmlWithGoogleAnalytics)
            {
                IsBodyHtml = true
            };

            new InlineImgHelperAttach().AttachImages(message);

            if (msg.Attachments != null) AppendAttachments(message, msg.Attachments.ToArray());

            if (deliveryNotification)
            {
                message.Headers.Add("Disposition-Notification-To", msg.SenderAddress);
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess |
                                                      DeliveryNotificationOptions.OnFailure;
            }

            client.Send(message);

            return DateTime.Now;
        }

        private void AppendAttachments(MailMessage mMsg, RequestAttachment[] attachmenst)
        {
            foreach (var a in attachmenst)
            {
                mMsg.Attachments.Add(new Attachment(new MemoryStream(a.Data), a.Name, a.ContentTypeName));
            }
        }

        private string AppendNullPixel(string html, int messageId)
        {
            if (html == null)
                return html;

            int index = html.LastIndexOf("</body>", StringComparison.OrdinalIgnoreCase);
            if (index == -1) return html;

            return html.Insert(index, string.Format("<img src=\"{0}={1}\"/>", _nullPixelUrl, messageId));
        }

        private string AppendGoogleAnalytics(string html, int messageId)
        {
            if (html == null)
                return html;

            int index = html.LastIndexOf("</body>", StringComparison.OrdinalIgnoreCase);
            if (index == -1) return html;

            var src = string.Format("http://www.google-analytics.com/collect?v=1&tid=UA-88038841-1&cid={0}&t=event&ec=email&ea=open&el=*|UNIQID|*&cs={1}&cm=email&cn={2}&cm1=1", messageId, messageId, messageId);

            return html.Insert(index, string.Format("<img src=\"{0}\"/>", src));
        }
    }
}