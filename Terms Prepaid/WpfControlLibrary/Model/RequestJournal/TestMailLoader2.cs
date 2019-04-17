using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ImapX.Enums;
using terms_prepaid.Helper_Classes;
using WpfControlLibrary.Helpers;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Загрузчик новых писем на ImapX
    /// </summary>
    public class TestMailLoader2 : ImapXRequestMessageLoaderBase
    {
        /// <summary>
        /// Get all new messages
        /// </summary>
        /// <returns></returns>
        public override List<RequestMessageThread> GetMessages()
        {
            if(!ClientIsOk()) 
                throw new Exception("Почтовый клиент недоступен");

            return GetThreads(_client);
        }

        private List<RequestMessageThread> GetThreads(ImapX.ImapClient client)
        {
            client.Behavior.MessageFetchMode = MessageFetchMode.Full;
            client.Folders.Inbox.Messages.Download();
            client.Folders.Inbox.Messages.Download("ALL", MessageFetchMode.Attachments);

            return client.Folders.Inbox.Messages.Select(msg => AttachThread(client, msg)).ToList();
        }

        public RequestMessageThread AttachThread(ImapX.ImapClient client, ImapX.Message msg)
        {
            string text;
            string html;
            var mThread = new RequestMessageThread();

            if(msg.Date == null)
                throw new Exception(string.Format("Message UId={0} error: date is null", msg.UId));

            try
            {
                text = ReplaceTrash(msg.Body.Text);
            }
            catch (FormatException e)
            {
                var content = PrivateFiledGetter.GetInstanceField(typeof(ImapX.MessageBody), msg.Body, "_textContent") as ImapX.MessageContent;
                var htmlArray = Convert.FromBase64String(ReplaceTrash2(content.ContentStream));
                text = Encoding.UTF8.GetString(htmlArray);
            }

            try
            {
                html = ReplaceTrash(msg.Body.Html);
            }
            catch (FormatException e)
            {
                var content = PrivateFiledGetter.GetInstanceField(typeof(ImapX.MessageBody), msg.Body, "_htmlContent") as ImapX.MessageContent;
                var htmlArray = Convert.FromBase64String(ReplaceTrash2(content.ContentStream));
                html = Encoding.UTF8.GetString(htmlArray);
            }

            html = AttachMessageContent(html, msg.BodyParts);
            
            mThread.Message = new RequestMessage
            {
                Id = (int)msg.UId,
                Text = "",
                Date = (DateTime)msg.Date,
                SenderAddress = msg.From.Address,
                DestinationAddress = msg.To.First().Address,
                IsIncomming = true,
                Reply = false,
                Theme = msg.Subject,
                Seen = false,
                Html = !string.IsNullOrEmpty(html) ? html : TextToHtml.Convert(text),
                Attachments = new ObservableCollection<RequestAttachment>(GetAttachments(msg))
            };

            return mThread;
        }

        private RequestAttachment[] GetAttachments(ImapX.Message msg)
        {
            return msg.Attachments.Select(GetAttachment).ToArray();
        }

        private RequestAttachment GetAttachment(ImapX.Attachment a)
        {
            byte[] data = null;
            try
            {
                data = Convert.FromBase64String(ReplaceTrash2(a.FileData.ToString()));
            }
            catch (FormatException e)
            {
                var content = PrivateFiledGetter.GetInstanceField(typeof(ImapX.Attachment), a, "_content") as ImapX.MessageContent;
                var stream = Convert.FromBase64String(ReplaceTrash2(content.ContentStream));
                //FileHelper.SaveToFileWithDialog(a.FileName, stream);
                data = stream;//Encoding.UTF8.GetString(stream).ToByteArray();
            }

            //FileHelper.SaveToFileWithDialog(a.FileName, data);

            return new RequestAttachment { Data = data, ContentType = a.ContentType, Name = a.FileName, ContentId = a.ContentId };
        }

        private string AttachMessageContent(string html, ImapX.MessageContent[] contentArray)
        {
            return contentArray.Where(c => c.ContentType.MediaType.IndexOf("image", StringComparison.Ordinal) != -1).Aggregate(html, AttachImageBase64);
        }

        public string AttachImageBase64(string html, ImapX.MessageContent content)
        {
            var stream = ReplaceTrash2(content.ContentStream);
            var contentId = Regex.Replace(content.ContentId, "([<,>])", "");
            
            var i = html.IndexOf(contentId, StringComparison.Ordinal);
            if (i == -1) throw new Exception(string.Format("AttachImageBase64 : ContentId={0} not found in html={1}", contentId, html));

            string oldStr = string.Format("src=\"cid:{0}", contentId);
            string newStr = string.Format("src=\"data:{0};base64,{1}", contentId, stream);
            return html.Replace(oldStr, newStr);
        }

        private string ReplaceTrash2(string text)
        {
            var trash = ")IMAPX";
            var index = text.IndexOf(trash);
            return index > -1 ?  text.Substring(0, index) : text;
        }

        private string ReplaceTrash(string text)
        {
            var trash = ")\r\nIMAPX";
            var index = text.IndexOf(trash);
            return index > -1 ?  text.Substring(0, index) : text;
        }

        private string GetText(string text)
        {
            var index = text.IndexOf(STATUS, StringComparison.Ordinal);
            return index > -1 ? text.Substring(0, index) : text;
        }
    }
}