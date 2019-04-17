using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataService;
using GalaSoft.MvvmLight.Command;
using Utilities.DataTypes.ExtensionMethods;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Util;
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
        private IWindowsHelper _windowsHelper;
        //private IRequestJournalLoader _loader;

        private ulong _lastTimeStamp;

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

        private int? _annulateStatusId ;
        private readonly RequestMessagesHelper _requestMessagesHelper;

        public RequestJournal(Action updateCallback)
        {
            _service = Repository.GetInstance<IRequestJournalService>();
            _updateCallback = updateCallback;
            _requestMessagesHelper = new RequestMessagesHelper();
            _windowsHelper = Repository.GetInstance<IWindowsHelper>();

            var userService = Repository.GetInstance<IUsersService>();
            Managers = new ObservableCollection<User>(ManagerHelper.Instance.Managers);

            _annulateStatusId = _service.GetAnnulateStatusId();

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

        private void Annulate(Request request)
        {
            if (request != null)
            {
                _windowsHelper.ShowWindow(new SelectCancelationReasonViewModel(request.Number, request.UsKey), Repository.GetInstance<ISelectCancelationReasonView>());

                //request.RequestStatuses = new ObservableCollection<RequestStatus>(GetRequestStatuses(request.Number));
                //request.StatusDictionary = new ObservableCollection<RequestStatus> { request.Status };
            }
        }

        public void SetPerformer(Request request, string performer, User[] managers)
        {
            request.Performer = performer;
            var manager = managers.FirstOrDefault(m => m.Name.Equals(performer, StringComparison.Ordinal));
            var managerId = manager != null ? manager.Key : (int?)null;

            if (managerId == null)
                TpLogger.ErrorWithMessage("Ошибка назначения исполнителя", string.Format("Исполнитель не найден: Name={0}", performer));
            else
                _service.AddRequestStatus(request.Number, 1, managerId, DateTime.Now);
        }

        public void SetRequests(DataTable dt, ulong timeStamp)
        {
            var requestsTemp = dt.Select().ToArray();
            var requests = requestsTemp.Select(GetRequest).ToArray();
            Requests = new ObservableCollection<Request>(requests);

            _lastTimeStamp = timeStamp;
        }

        public void LoadRequests(bool cancellate, bool reserved, ulong timeStamp)
        {
            var r = _service.GetAllRequests(cancellate, reserved, ulong.MinValue);
            SetRequests(r, timeStamp);
        }

        public void UpdateRequests(bool cancellate, bool reserved, ulong timeStamp)
        {
            var newRequests = _service.GetAllRequests(cancellate, reserved, _lastTimeStamp).Select().Select(GetRequest).ToArray();
            var currentRequests = Requests.ToArray();

            foreach (var request in newRequests)
            {
                var i = Array.FindIndex(currentRequests, r => r.Number == request.Number);
                currentRequests[i] = request;
            }

            Requests = new ObservableCollection<Request>(currentRequests);

            _lastTimeStamp = timeStamp;
        }

        private Request GetRequest(DataRow row)
        {
            var requestId = row.Field<int>("Id");

            //TpLogger.WriteElapsedMs(string.Format("0. Id = {0}", requestId));

            int usKey = row.Field<int?>("US_KEY") ?? -1;

            var manager = Managers.FirstOrDefault(m => m.Key == usKey);
            var managerName = manager != null ? manager.Name : "Не назначен";
            
            //TpLogger.WriteElapsedMs("1");

            var request = new Request(Annulate)
            {
                Number = requestId,
                Date = row.Field<DateTime>("Date"),
                RequestTypeId = row.Field<int>("RequestTypeId"),
                RequestTypeStr = row.Field<string>("RequestTypeStr"),
                StatusId = row.Field<int>("RequestStatusId"),
                StatusStr = row.Field<string>("RequestStatusStr"),
                UsKey = usKey,
                UserName = row.Field<string>("UserName"),
                RequestSubStatuses = new ObservableCollection<RequestSubStatus>(GetRequestSubStatuses(requestId)),
                Messages = new ObservableCollection<RequestMessage>(/*_requestMessagesHelper.GetMessages(requestId)*/),
                Performer = managerName,
                DgCode = row.Field<string>("DgCode"),
                SuperviserChecked = row.Field<DateTime?>("SuperviserChecked"),
                IsClosed = row.Field<bool?>("IsClosed") ?? false,
                SenderAddress = row.Field<string>("SenderAddress"),
                Theme = row.Field<string>("Theme"),
                MtMInfo = new RequestMessagInfoFactory().GetMtMInfo(row),
                MtCInfo = new RequestMessagInfoFactory().GetMtCInfo(row),
                Source = row.Field<byte>("Source")
            };

           // TpLogger.WriteElapsedMs("2");

            var statuses = new ObservableCollection<RequestStatus>(GetRequestStatuses(requestId));
            var status = statuses.LastOrDefault();

            //TpLogger.WriteElapsedMs("3");

            if(status == null)
                throw new Exception(string.Format("Статус запроса №{0} не установлен", request.Number));

            // Текущий статус запроса
            var statusDictionary = new ObservableCollection<RequestStatus> { status };

            var substatusDictionary = new ObservableCollection<RequestSubStatus> { request.SubStatus };

            //TpLogger.WriteElapsedMs("4");

            // Если статус не "Аннулирован", добавить статус "Аннулировать"
            //if (status.Id != _service.GetAnnulateStatusId())
            //    statusDictionary.Add(new RequestStatus(Request.AnnulateId, "Аннулировать", DateTime.MinValue));
            
            // Если статус не "Аннулирован", добавить статус "Аннулировать"
            if (status.Id != _annulateStatusId)
                substatusDictionary.Add(new RequestSubStatus(Request.AnnulateId, Request.AnnulateId, "Аннулировать", DateTime.MinValue));

           // TpLogger.WriteElapsedMs("5");

            request.SubstatusDictionary = substatusDictionary;
            request.StatusDictionary = statusDictionary;
            request.RequestStatuses = statuses;

            //TpLogger.WriteElapsedMs("6");

            request.UpdateMessageCounts();

            //TpLogger.WriteElapsedMs("7");

            return request;
        }

        private RequestStatus[] GetRequestStatuses_old(int requestId)
        {
            var statusHistory = _service.GetRequestStatusHistory(requestId);

            return (from DataRow row in statusHistory.Rows select new RequestStatus(row.Field<int>("RequestStatusId"), row.Field<string>("Name"), 
                row.Field<DateTime>("Date"))).ToArray();
        }

        private RequestStatus[] GetAllRequestStatuses()
        {
            return _service.GetAllStatuses()
                .Select()
                .Where(r => r.Field<bool>("IsSelectable"))
                .Select(r => new RequestStatus(r.Field<int>("Id"), r.Field<string>("Name"), DateTime.MinValue))
                .ToArray();
        }

        private RequestStatus[] GetRequestStatuses(int requestId)
        {
            var statusHistory = _service.GetRequestStatusHistory2(requestId);
            
            return (from DataRow row in statusHistory.Rows
                    select new RequestStatus(row.Field<int>("RequestStatusId"), row.Field<string>("Name"),
                        row.Field<DateTime>("Date"), row.Field<int?>("UserId"), row.Field<string>("UsName"))).ToArray();
        }

        private RequestSubStatus[] GetRequestSubStatuses(int requestId)
        {
            //TpLogger.WriteElapsedMs("1.1.0");

            var statusHistory = _service.GetRequestSubStatusHistory2(requestId);

            //TpLogger.WriteElapsedMs("1.1.1");

            var result = (from DataRow row in statusHistory.Rows
                    select new RequestSubStatus(row.Field<int?>("RequestSubStatusId") ?? 0, row.Field<int>("RequestStatusId"), row.Field<string>("Name"),
                        row.Field<DateTime>("Date"), row.Field<byte?>("ColorIndex") ?? 0, row.Field<bool?>("Alarm") ?? false)).ToArray();

            //TpLogger.WriteElapsedMs("1.1.2");

            return result;
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