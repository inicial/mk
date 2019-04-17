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

        private readonly int? _annulateStatusId ;
        private readonly RequestMessagesHelper _requestMessagesHelper;

        public RequestJournal(Action updateCallback)
        {
            _service = Repository.GetInstance<IRequestJournalService>();
            _updateCallback = updateCallback;
            _requestMessagesHelper = new RequestMessagesHelper();
            _windowsHelper = Repository.GetInstance<IWindowsHelper>();

            Managers = new ObservableCollection<User>(ManagerHelper.Instance.Managers);

            _annulateStatusId = _service.GetAnnulateStatusId();

        }

        private void Annulate(Request request)
        {
            if (request != null)
                _windowsHelper.ShowWindow(new SelectCancelationReasonViewModel(request.Number, request.UsKey), Repository.GetInstance<ISelectCancelationReasonView>(), true, true);
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

        public void SetRequests(DataRow[] rows, ulong timeStamp)
        {
            var requestsTemp = rows.ToArray();
            var requests = requestsTemp.Select(GetRequest).OrderByDescending(r => r.Caption).ToArray();
            Requests = new ObservableCollection<Request>(requests);

            _lastTimeStamp = timeStamp;
        }

        public void LoadRequests(bool cancellate, bool reserved, ulong timeStamp)
        {
            var r = _service.GetAllRequests(cancellate, reserved, ulong.MinValue);
            SetRequests(r.Select(), timeStamp);
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

            int usKey = row.Field<int?>("US_KEY") ?? -1;

            var manager = Managers.FirstOrDefault(m => m.Key == usKey);
            var managerName = manager != null ? manager.Name : "Не назначен";

            var mainN = row.Field<string>("mainN");

            var subStatus = new RequestSubStatus(row.Field<int?>("rss.RequestSubStatusId") ?? 0,
                row.Field<int?>("rss.RequestStatusId") ?? 0, row.Field<string>("rss.Name"),
                row.Field<DateTime>("rss.Date"), row.Field<byte?>("rss.ColorIndex") ?? 0,
                row.Field<bool?>("rss.Alarm") ?? false);

            var status = new RequestStatus(row.Field<int>("rs.RequestStatusId"), row.Field<string>("rs.Name"),
                row.Field<DateTime>("rs.Date"), row.Field<int?>("rs.UserId"), row.Field<string>("rs.UsName"));

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
                //RequestSubStatuses = new ObservableCollection<RequestSubStatus>(GetRequestSubStatuses(requestId)),
                Messages = new ObservableCollection<RequestMessage>(),
                Performer = managerName,
                DgCode = row.Field<string>("DgCode"),
                SuperviserChecked = row.Field<DateTime?>("SuperviserChecked"),
                IsClosed = row.Field<bool?>("IsClosed") ?? false,
                SenderAddress = row.Field<string>("SenderAddress"),
                Theme = row.Field<string>("Theme"),
                MtMInfo = new RequestMessagInfoFactory().GetMtMInfo(row),
                MtCInfo = new RequestMessagInfoFactory().GetMtCInfo(row),
                Source = row.Field<byte>("Source"),
                MainN = mainN,
                Caption = mainN ?? requestId.ToString(),
                SubStatus = subStatus,
                Status = status
            };

            //var statuses = new ObservableCollection<RequestStatus>(GetRequestStatuses(requestId));
            //var status = statuses.LastOrDefault();

            //if(status == null)
            //    throw new Exception(string.Format("Статус запроса №{0} не установлен", request.Number));

            var statusDictionary = new ObservableCollection<RequestStatus> { request.Status };

            var substatusDictionary = new ObservableCollection<RequestSubStatus> { request.SubStatus };

            if (request.Status.Id != _annulateStatusId)
                substatusDictionary.Add(new RequestSubStatus(Request.AnnulateId, Request.AnnulateId, "Аннулировать", DateTime.MinValue));

            request.SubstatusDictionary = substatusDictionary;
            request.StatusDictionary = statusDictionary;
            //request.RequestStatuses = statuses;

            request.UpdateMessageCounts();

            return request;
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

            var statusHistory = _service.GetRequestSubStatusHistory2(requestId);

            var result = (from DataRow row in statusHistory.Rows
                    select new RequestSubStatus(row.Field<int?>("RequestSubStatusId") ?? 0, row.Field<int>("RequestStatusId"), row.Field<string>("Name"),
                        row.Field<DateTime>("Date"), row.Field<byte?>("ColorIndex") ?? 0, row.Field<bool?>("Alarm") ?? false)).ToArray();

            return result;
        }

    }
}