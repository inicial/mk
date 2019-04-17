using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using DataService;
using WpfControlLibrary.Common;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Журнал заявок
    /// </summary>
    public class RequestJournal : Data
    {
        private Action _updateCallback;

        private IRequestJournalService _service;
        //private IRequestJournalLoader _loader;

        private ObservableCollection<User> _managers;
        public ObservableCollection<User> Managers
        {
            get { return _managers; }
            set { SetValue(ref _managers, value); }
        }

        private ObservableCollection<Request> _requests;
        public ObservableCollection<Request> Requests
        {
            get { return _requests; }
            set { SetValue(ref _requests, value); }
        }

        private ObservableCollection<RequestMessage> _requestMessages;
        public ObservableCollection<RequestMessage> RequestMessages
        {
            get { return _requestMessages; }
            set { SetValue(ref _requestMessages, value); }
        }

        public RequestJournal(Action updateCallback, ObservableCollection<User> managers = null, IRequestJournalService service = null)
        {
            _updateCallback = updateCallback;
            _service = service ?? Repository.GetInstance<IRequestJournalService>();

            var userService = Repository.GetInstance<IUsersService>();
            Managers = managers ?? new ObservableCollection<User>(userService.GetManagerList()
                .Select()
                .Select(row => new User(DataRowExtensions.Field<int>(row, "us_key"), DataRowExtensions.Field<string>(row, "US_FullNameLat"), DataRowExtensions.Field<string>(row, "US_MAILBOX"), DataRowExtensions.Field<int?>(row, "Phone"))));

            //_loader = Repository.GetInstance<IRequestJournalLoader>();
            //_loader.Connect(Repository.GetInstance<IMailConfig>());
            //_loader.SetNewMessageNotificationsCallback(_updateCallback);
        }

        /*
        public void LoadNewMessages()
        {
            if (!_loader.ClientIsOk())
                _loader.Connect(Repository.GetInstance<IMailConfig>());

            var threads = _loader.GetMessages();
            
            foreach (var t in threads)
                AttachThread(t);

            _loader.RemoveMessages();
        }
        */

        public void SetPerformer(Request request, string performer, User[] managers)
        {
            request.Performer = performer;
            var manager = managers.FirstOrDefault(m => m.Name.Equals(performer, StringComparison.Ordinal));
            var managerId = manager != null ? manager.Key : (int?)null;

            _service.AddRequestStatus(request.Number, 1, managerId, DateTime.Now);
        }

        public void GetRequests()
        {
            Requests = new ObservableCollection<Request>(_service.GetAllRequests().Select().Select(GetRequest));
        }

        private Request GetRequest(DataRow row)
        {
            var requestId = row.Field<int>("Id");

            int usKey = row.Field<int?>("US_KEY") ?? -1;

            var manager = Managers.FirstOrDefault(m => m.Key == usKey);
            var managerName = manager != null ? manager.Name : "Не назначен";

            var request = new Request
            {
                Number = requestId,
                Date = row.Field<DateTime>("Date"),
                RequestTypeId = row.Field<int>("RequestTypeId"),
                RequestTypeStr = row.Field<string>("RequestTypeStr"),
                StatusId = row.Field<int>("RequestStatusId"),
                StatusStr = row.Field<string>("RequestStatusStr"),
                UsKey = usKey,
                UserName = row.Field<string>("UserName"),
                RequestStatuses = new ObservableCollection<RequestStatus>(GetRequestStatuses(requestId)),
                Messages = new ObservableCollection<RequestMessage>(GetMessages(requestId)),
                Performer = managerName
            };

            request.UpdateMessageCounts();

            return request;
        }

        private User GetManager(int? usKey, string eMail)
        {
            return usKey != null ? GetManager((int) usKey) : GetManager(eMail);
        }

        private User GetManager(string eMail)
        {
            return Managers.FirstOrDefault(m => m.Email == eMail);
        }

        private User GetManager(int usKey)
        {
            return Managers.FirstOrDefault(m => m.Key == usKey);
        }

        private RequestMessage[] GetMessages(int requestId)
        {
            var messages = _service.GetMessages(requestId);
            return messages.Select().Select(GetMessage).ToArray();
        }

        private RequestMessage GetMessage(DataRow row)
        {
            var id = row.Field<int>("Id");

            return new RequestMessage
            {
                Id = id,
                Text = row.Field<string>("Text"),
                Date = row.Field<DateTime>("Date"),
                RequestId = row.Field<int>("RequestId"),
                SenderAddress = row.Field<string>("SenderAddress"),
                DestinationAddress = row.Field<string>("DestinationAddress"),
                Seen = row.Field<bool>("Seen"),
                ReadDate = row.Field<DateTime?>("ReadDate"),
                Theme = row.Field<string>("Theme"),
                IsIncomming = row.Field<bool>("IsIncoming"),
                InReplyToId = row.Field<int>("InReplyToId"),
                Reply = row.Field<bool>("Reply"),
                Html = row.Field<string>("Html"),
                Mod = row.Field<string>("Mod"),
                User = GetManager(row.Field<int?>("UsKey"), row.Field<string>("SenderAddress")),
                Tracking = row.Field<DateTime?>("Tracking"),
                Sent = row.Field<DateTime?>("Sent"),
                Attachments = new ObservableCollection<RequestAttachment>(GetAttachments(id))
            };
        }

        private RequestAttachment[] GetAttachments(int requestMessageId)
        {
            return _service.GetAttachments(requestMessageId).Select().Select(GetAttachment).ToArray();
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

        private RequestStatus[] GetRequestStatuses(int requestId)
        {
            var statusHistory = _service.GetRequestStatusHistory(requestId);

            return (from DataRow row in statusHistory.Rows select new RequestStatus(row.Field<int>("RequestStatusId"), row.Field<string>("Name"), 
                row.Field<DateTime>("Date"))).ToArray();
        }

        private void AttachThread(RequestMessageThread thread)
        {
            int? requestId = _service.GetRequestByMessageId(thread.Message.Id);
            if (requestId == -1) requestId = null;

            AddMessages(thread, null);
        }

        private void AddMessages(RequestMessageThread thread, int? requestId)
        {
            var msg = thread.Message;

            var requestIdInTheme = GetRequestId(msg.Theme);
            if (requestIdInTheme != -1 && _service.RequestIdIsExists(requestIdInTheme))
            {
                requestId = requestIdInTheme;
            }

            var mod = Managers.FirstOrDefault(m => m.Email.Equals(msg.SenderAddress, StringComparison.OrdinalIgnoreCase)) != null ? RequestMessageMod.MTM : RequestMessageMod.MTC;
            
            int realId = _service.AddMessage(msg.Id, requestId, msg.Text, msg.Date, msg.SenderAddress, msg.DestinationAddress, msg.Seen, msg.ReadDate, msg.Theme,
                msg.IsIncomming, msg.InReplyToId, msg.Reply, msg.Html, mod, msg.User != null ? msg.User.Key : (int?)null);

            if (realId != -1) AddAttachments(msg);
        }

        private void AddAttachments(RequestMessage msg)
        {
            foreach (var a in msg.Attachments)
                _service.AddAttachment(msg.Id, a.ContentType.MediaType, a.Name, a.Data);
        }

        private int GetRequestId(string theme)
        {
            string pattern = @"\b(Заявка №\d+)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(theme);

            if (match.Success)
            {
                string pattern2 = @"№(\d+)";
                Regex regex2 = new Regex(pattern2);

                Match match2 = regex2.Match(match.Groups[1].Value);
                if (match2.Success)
                {
                    return Convert.ToInt32(match2.Groups[1].Value);
                }
            }

            return -1;
        }

    }
}