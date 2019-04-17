using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class RequestMessagesHelper
    {
        private readonly IRequestJournalService _service;

        public RequestMessagesHelper()
        {
            _service = Repository.GetInstance<IRequestJournalService>();
        }

        public RequestMessage[] GetMessages(int requestId)
        {
            //TpLogger.WriteElapsedMs("1.2.0");
            var messages = _service.GetMessages(requestId);
            //TpLogger.WriteElapsedMs("1.2.1");
            var messagesTemp = messages.Select().ToArray();
            //TpLogger.WriteElapsedMs("1.2.2");
            var result = Enumerable.ToArray<RequestMessage>(messagesTemp.Select(GetMessage));
            //TpLogger.WriteElapsedMs("1.2.3");
            return result;
        }

        private RequestMessage GetMessage(DataRow row)
        {
            var id = row.Field<int>("Id");

            var senderAddress = new MailAddressHelper().GetMails(row.Field<string>("SenderAddress")).FirstOrDefault();
            
            return new RequestMessage
            {
                Id = id,
                Text = row.Field<string>("Text"),
                Date = row.Field<DateTime>("Date"),
                RequestId = row.Field<int>("RequestId"),
                SenderAddress = senderAddress,
                DestinationAddress = row.Field<string>("DestinationAddress"),
                Seen = row.Field<bool>("Seen"),
                ReadDate = row.Field<DateTime?>("ReadDate"),
                Theme = row.Field<string>("Theme"),
                IsIncomming = row.Field<bool>("IsIncoming"),
                InReplyToId = row.Field<int>("InReplyToId"),
                Reply = row.Field<bool>("Reply"),
                Html = row.Field<string>("Html"),
                Mod = row.Field<string>("Mod"),
                User = ManagerHelper.Instance.GetManager(ManagerHelper.Instance.Managers, row.Field<int?>("UsKey"), senderAddress),
                Tracking = row.Field<DateTime?>("Tracking"),
                Forwarded = row.Field<bool?>("Forwarded") ?? false,
                Sent = row.Field<DateTime?>("Sent"),
                Attachments = new ObservableCollection<RequestAttachment>(GetAttachments(id))
            };
        }

        private RequestAttachment[] GetAttachments(int requestMessageId)
        {
            return Enumerable.ToArray<RequestAttachment>(_service.GetAttachments(requestMessageId).Select().Select(GetAttachment));
        }

        private RequestAttachment GetAttachment(DataRow row)
        {
            return new RequestAttachment
            {
                Id = row.Field<int>("Id"),
                RequestMessageId = row.Field<int>("RequestMessageId"),
                RequestAttachmentTypeId = row.Field<int>("RequestAttachmentTypeId"),
                Name = row.Field<string>("Name"),
                Data = row.Field<byte[]>("Data"),
                ContentTypeName = row.Field<string>("ContentType")
            };
        }
    }
}