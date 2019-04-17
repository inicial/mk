using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfControlLibrary.Model.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Фабрика новых сообщений
    /// </summary>
    public class RequestMessageBuilder
    {
        public static RequestMessage GetRequestMessage(User user, Request request, RequestMessage replyTo, string text)
        {
            if(request == null || replyTo == null) throw new Exception("Ошибка ответа на запрос: сообщение запроса отсутствует");

            return new RequestMessage()
            {
                Date = DateTime.Now,
                SenderAddress = replyTo.DestinationAddress,
                DestinationAddress = replyTo.SenderAddress,
                Theme = GetTheme(request.Number),
                InReplyToId = replyTo.Id,
                IsIncomming = false,
                Reply = true,
                RequestId = request.Number,
                Id = 0,
                ReadDate = null,
                Text = text,
                Seen = true,
                User = user
            };
        }

        public static RequestMessage GetRequestMessageHtml(User user, string from, string to, string subject, Request request, RequestMessage replyTo, string html, string signature, string mod,
            IEnumerable<RequestAttachment> attachments)
        {
            if (request == null) throw new Exception("Ошибка ответа на запрос: сообщение запроса отсутствует");

            return new RequestMessage()
            {
                Date = DateTime.Now,
                SenderAddress = from,
                DestinationAddress = to,
                Theme = subject,
                InReplyToId = replyTo != null ? replyTo.Id : -1,
                IsIncomming = false,
                Reply = replyTo != null,
                RequestId = request.Number,
                Id = 0,
                ReadDate = null,
                Html = AppendSignature(html, signature),
                Seen = true,
                Mod = mod,
                User = user,
                Attachments = new ObservableCollection<RequestAttachment>(attachments)
            };
        }

        public static RequestMessage GetRequestMessageHtml(User user, Request request, RequestMessage replyTo, string html, string signature, string mod, 
            IEnumerable<RequestAttachment> attachments)
        {
            if (request == null) throw new Exception("Ошибка ответа на запрос: сообщение запроса отсутствует");

            return new RequestMessage()
            {
                Date = DateTime.Now,
                SenderAddress = replyTo.DestinationAddress,
                DestinationAddress = replyTo.SenderAddress,
                Theme = GetTheme(request.Number),
                InReplyToId = replyTo != null ? replyTo.Id : -1,
                IsIncomming = false,
                Reply = replyTo != null,
                RequestId = request.Number,
                Id = 0,
                ReadDate = null,
                Html = AppendSignature(html, signature),
                Seen = true,
                Mod = mod,
                User = user,
                Attachments = new ObservableCollection<RequestAttachment>(attachments)
            };
        }

        public static RequestMessage GetRequestMessageHtml(User user, Request request, string html, string signature, string from, string to, string mod, 
            IEnumerable<RequestAttachment> attachments, RequestMessage replyTo = null)
        {
            if (request == null) throw new Exception("Ошибка отправки сообщения: запрос отсутствует");

            return new RequestMessage()
            {
                Date = DateTime.Now,
                SenderAddress = from,
                DestinationAddress = to,
                Theme = GetTheme(request.Number),
                InReplyToId = replyTo != null ? replyTo.Id : -1,
                IsIncomming = false,
                Reply = replyTo != null,
                RequestId = request.Number,
                Id = 0,
                ReadDate = null,
                Html = AppendSignature(html, signature),
                Seen = true,
                Mod = mod,
                User = user,
                Attachments = new ObservableCollection<RequestAttachment>(attachments)
            };
        }

        private static string AppendSignature(string html, string signature)
        {
            if(html == null || signature == null)
                return html;

            int index = html.LastIndexOf("</body>", StringComparison.OrdinalIgnoreCase);
            if (index == -1) return html;

            return html.Insert(index, string.Format("<BR>---{0}", signature));
        }

        public static string GetTheme(int requestNumber)
        {
            return string.Format("Заявка №{0}", requestNumber);
        }
    }
}