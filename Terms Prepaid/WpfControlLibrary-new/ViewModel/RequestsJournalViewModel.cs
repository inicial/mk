using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataService;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Messages;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Model.Messages;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;
using WpfControlLibrary.View;
using Xceed.Wpf.DataGrid;
using System.Timers;
using DataRow = System.Data.DataRow;

namespace WpfControlLibrary.ViewModel
{
    public interface IRequestsJournalViewModel
    {
        void ChangeSenderAddress(string newSenderAddress);
        void ShowDataGrid();
    }

    public class RequestsJournalViewModelBase : ViewModelBase, IRequestsJournalViewModel
    {
        protected object _updateLock = new object();

        private Timer _timer;
        private volatile bool _requestStop = false;

        private int _problemsCountAll;
        public int ProblemsCountAll
        {
            get { return _problemsCountAll; }
            set { SetValue(ref _problemsCountAll, value); }
        }

        private int _problemsCountSelf;
        public int ProblemsCountSelf
        {
            get { return _problemsCountSelf; }
            set { SetValue(ref _problemsCountSelf, value); }
        }

        private int _newRequestCount;
        public int NewRequestCount
        {
            get { return _newRequestCount; }
            set { SetValue(ref _newRequestCount, value); }
        }

        private int _myRequestCount;
        public int MyRequestCount
        {
            get { return _myRequestCount; }
            set { SetValue(ref _myRequestCount, value); }
        }

        private ProblemRequestsViewModel _problemRequestsViewModel;
        public ProblemRequestsViewModel ProblemRequestsViewModel
        {
            get { return _problemRequestsViewModel; }
            set { SetValue(ref _problemRequestsViewModel, value); }
        }

        private const int _allId = -1;
        private const int _allProblemsId = -2;

        private RequestJournal _requestJournal;
        public RequestJournal RequestJournal
        {
            get { return _requestJournal; }
            set { SetValue(ref _requestJournal, value); }
        }

        private ObservableCollection<Request> _requests;
        public ObservableCollection<Request> Requests
        {
            get { return _requests; }
            set { SetValue(ref _requests, value); }
        }

        private ComboBoxFilter<string> _messageStatusFilter;
        public ComboBoxFilter<string> MessageStatusFilter
        {
            get { return _messageStatusFilter; }
            set { SetValue(ref _messageStatusFilter, value); }
        }

        private ComboBoxFilter<RequestStatus> _statusFilter;
        public ComboBoxFilter<RequestStatus> StatusFilter
        {
            get { return _statusFilter; }
            set { SetValue(ref _statusFilter, value); }
        }

        private ComboBoxFilter<RequestSubStatus> _subStatusFilter;
        public ComboBoxFilter<RequestSubStatus> SubStatusFilter
        {
            get { return _subStatusFilter; }
            set { SetValue(ref _subStatusFilter, value); }
        }

        private DateFilter _dateFilter;
        public DateFilter DateFilter
        {
            get { return _dateFilter; }
            set { SetValue(ref _dateFilter, value); }
        }

        private ComboBoxFilter<User> _performerFilter;
        public ComboBoxFilter<User> PerformerFilter
        {
            get { return _performerFilter; }
            set { SetValue(ref _performerFilter, value); }
        }

        private ComboBoxFilter<string> _clientFilter;
        public ComboBoxFilter<string> ClientFilter
        {
            get { return _clientFilter; }
            set { SetValue(ref _clientFilter, value); }
        }

        private ComboBoxFilter<RequestProblem> _problemsFilter;
        public ComboBoxFilter<RequestProblem> ProblemsFilter
        {
            get { return _problemsFilter; }
            set { SetValue(ref _problemsFilter, value); }
        }

        private bool _popupIsOpen;
        public bool PopupIsOpen
        {
            get { return _popupIsOpen; }
            set { SetValue(ref _popupIsOpen, value); }
        }

        private ObservableCollection<int> _requestIdCollection;
        public ObservableCollection<int> RequestIdCollection
        {
            get { return _requestIdCollection; }
            set { SetValue(ref _requestIdCollection, value); }
        }

        protected Request _selectedItem;
        public Request SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetValue(ref _selectedItem, value);
                new Task(UpdateCorrespondence).Start();
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetValue(ref _selectedIndex, value); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetValue(ref _userName, value); }
        }

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set { SetValue(ref _userId, value); }
        }

        private IAccessService _accessService;
        public IAccessService AccessService
        {
            get { return _accessService; }
            set { SetValue(ref _accessService, value); }
        }

        private bool _isSuperviser;
        public bool IsSuperviser
        {
            get { return _isSuperviser; }
            set { SetValue(ref _isSuperviser, value); }
        }

        private bool _hideAllRows;
        public bool HideAllRows
        {
            get { return _hideAllRows; }
            set { SetValue(ref _hideAllRows, value); }
        }

        private bool _showCorrespondenceWithManager;
        public bool ShowCorrespondenceWithManager
        {
            get { return _showCorrespondenceWithManager; }
            set { SetValue(ref _showCorrespondenceWithManager, value); }
        }

        private bool _showCorrespondenceWithClient;
        public bool ShowCorrespondenceWithClient
        {
            get { return _showCorrespondenceWithClient; }
            set { SetValue(ref _showCorrespondenceWithClient, value); }
        }

        private ObservableCollection<User> _managers;
        public ObservableCollection<User> Managers
        {
            get { return _managers; }
            set { SetValue(ref _managers, value); }
        }

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetValue(ref _user, value); }
        }

        private bool _hideCancelations;
        public bool HideCancelations
        {
            get { return _hideCancelations; }
            set
            {
                SetValue(ref _hideCancelations, value);
                //Update(true);
                //UpdateWithFilters();
            }
        }

        private bool _hideReserved;
        public bool HideReserved
        {
            get { return _hideReserved; }
            set
            {
                SetValue(ref _hideReserved, value);
                //Update(true);
                //UpdateWithFilters();
            }
        }

        private string _senderAddressFilter;
        public string SenderAddressFilter
        {
            get { return _senderAddressFilter; }
            set { SetValue(ref _senderAddressFilter, value); }
        }

        private RequestStatus[] _statuses;
        private RequestProblem[] _problems;

        ulong _timestamp;

        protected ISimpleWindows _appointPerformerWindows;

        public RelayCommand TakeAJobCommand { get; set; }
        public RelayCommand AppointPerformerCommand { get; set; }
        public RelayCommand<int> BindingButtonCommand { get; set; }
        public RelayCommand ShowCorrespWithClientCommand { get; set; }
        public RelayCommand ShowCorrespWithManagerCommand { get; set; }
        public RelayCommand ShowDataGridCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand ClearFilterCommand { get; set; }

        public RelayCommand<int> ShowCorrespondenceCommand { get; set; }

        public RelayCommand ShowNewRequestsCmd { get; set; }
        public RelayCommand ShowAllRequestsCmd { get; set; }
        public RelayCommand ShowAllMyRequestsCmd { get; set; }
        public RelayCommand ShowNeedReplyRequestsCmd { get; set; }

        public RelayCommand ShowAllProblemRequestsCmd { get; set; }
        public RelayCommand ShowSelfProblemRequestsCmd { get; set; }

        public RelayCommand ApplySenderFilterCmd { get; set; }

        private IRequestJournalService _service;
        private IWindowsHelper _winHelper;
        private bool _showOnlyMyRequests;
        private bool _noUpdate;
        
        public RequestsJournalViewModelBase()
        {
            _service = Repository.GetInstance<IRequestJournalService>();
            _winHelper = Repository.GetInstance<IWindowsHelper>();

            _statuses = GetStatuses();
            _problems = GetRequestProblems();
            
            TakeAJobCommand = new RelayCommand(TakeAJob, () => BindButtonEnabledConverter.GetBindPermission(SelectedItem));
            AppointPerformerCommand = new RelayCommand(AppointPerformer, () => BindButtonEnabledConverter.GetBindPermission(SelectedItem));
            BindingButtonCommand = new RelayCommand<int>(BindingButtonClick);
            ShowCorrespWithClientCommand = new RelayCommand(ShowCorrespWithClient, () => SelectedItem != null);
            ShowCorrespWithManagerCommand = new RelayCommand(ShowCorrespWithManager, () => SelectedItem != null);
            ShowDataGridCommand = new RelayCommand(ShowDataGrid);
            UpdateCommand = new RelayCommand(() => Update(true));
            ClearFilterCommand = new RelayCommand(ClearFilters);
            ShowCorrespondenceCommand = new RelayCommand<int>(ShowCorrespondence);

            ShowNewRequestsCmd = new RelayCommand(ShowNewRequests);
            ShowAllRequestsCmd = new RelayCommand(ShowAllRequests);
            ShowAllMyRequestsCmd = new RelayCommand(ShowAllMyRequests);
            ShowNeedReplyRequestsCmd = new RelayCommand(ShowNeedReplyRequests);

            ShowAllProblemRequestsCmd = new RelayCommand(() => ShowProblemRequests(false), () => ProblemsCountAll != 0);
            ShowSelfProblemRequestsCmd = new RelayCommand(() => ShowProblemRequests(true), () => ProblemsCountSelf != 0);

            ApplySenderFilterCmd = new RelayCommand(ApplySenderFilter, () => !string.IsNullOrEmpty(SenderAddressFilter));
            
            GetUserInfo();
            ProblemRequestsViewModel = new ProblemRequestsViewModel(_userId, OnRequestProblemGroupSelected);
            RequestJournal = new RequestJournal(() => Update());
            RegisterMessages();
            InitTimer(5);

            _hideCancelations = true;
            _hideReserved = true;
        }

        public virtual void UpdateCorrespondence()
        {
            
        }

        private void ApplySenderFilter()
        {
            UpdateWithFilters();
        }

        private void OnRequestProblemGroupSelected(RequestProblemGroup problemGroup)
        {
            if (problemGroup == null)
                return;

            ClearFilters();
            ProblemsFilter.Value = ProblemsFilter.Values.FirstOrDefault(p => p.ProblemId == problemGroup.ProblemId);
            Update(true);
        }

        private void ClearFilters()
        {
            _noUpdate = true;
            _showOnlyMyRequests = false;
            StatusFilter.Reset();
            ProblemsFilter.Reset();
            ClientFilter.Reset();
            PerformerFilter.Reset();
            SenderAddressFilter = string.Empty;
            DateFilter.Enabled = false;
            _noUpdate = false;
        }

        private void ShowNewRequests()
        {
            ClearFilters();
            StatusFilter.Value = StatusFilter.Values[1];
            Update(true);
        }

        private void ShowAllRequests()
        {
            ClearFilters();
            StatusFilter.Value = StatusFilter.Values[0];
            Update(true);
        }

        private void ShowAllMyRequests()
        {
            ClearFilters();
            _showOnlyMyRequests = true;
            StatusFilter.Value = StatusFilter.Values[0];
            Update(true);
        }

        private void ShowProblemRequests(bool selfOnly)
        {
            ProblemRequestsViewModel.UpdateProblems();
            ProblemRequestsViewModel.ShowSelfOnly = selfOnly;
            _winHelper.ShowWindow(ProblemRequestsViewModel, Repository.GetInstance<IProblemRequestsView>(), modal: true);
        }

        private void ShowNeedReplyRequests()
        {
            ClearFilters();
            ProblemsFilter.Value = ProblemsFilter.Values[4];
            Update(true);
        }

        private void ShowCorrespondence(int columnIndex)
        {
            if (columnIndex == 2 || columnIndex == 3 || columnIndex == 4)
                return;

            if (columnIndex < 9)
                ShowCorrespWithClient();
            else
                ShowCorrespWithManager();
        }

        private void SetFilterCallback()
        {
            //MessageStatusFilter.Callback = UpdateWithFilters;
            
            /*StatusFilter.Callback = () => UpdateWithFilters();
            SubStatusFilter.Callback = () => UpdateWithFilters();
            ClientFilter.Callback = () => UpdateWithFilters();
            PerformerFilter.Callback = () => UpdateWithFilters();
            DateFilter.Callback = () => UpdateWithFilters();
            ProblemsFilter.Callback = () => UpdateWithFilters();*/
        }

        private int? GetProblemsFilterOld()
        {
            return ProblemsFilter.Value != null && ProblemsFilter.Value.ProblemId != -1 ? ProblemsFilter.Value.ProblemId : (int?)null;
        }

        private int[] GetProblemsFilter()
        {
            return ProblemsFilter.Value == null || ProblemsFilter.Value.ProblemId == -1
                ? new int[0]
                : ProblemsFilter.Value.ProblemId == -2
                    ? _problems.Select(p => p.ProblemId).ToArray()
                    : new []{ ProblemsFilter.Value.ProblemId };
        }

        private string GetClientFilter(bool contains)
        {
            return contains 
                ? SenderAddressFilter
                : ClientFilter.Value != null && !ClientFilter.Value.Equals("Все", StringComparison.OrdinalIgnoreCase) ? ClientFilter.Value : null;
        }

        private int? GetPerformerFilter()
        {
            return PerformerFilter.Value != null && PerformerFilter.Value.Key != -1
                ? PerformerFilter.Value.Key
                : _showOnlyMyRequests ? UserId : (int?)null;
        }

        private int[] GetStatusFilter()
        {
            return StatusFilter.Value != null && StatusFilter.Value.Id != -1 ?
                new[] { StatusFilter.Value.Id } :
                _statuses.Select(s => s.Id).Where(id => !(HideReserved && id == 3) && !(HideCancelations && id == 4)).ToArray();
        }

        private void UpdateWithFilters(RequestJournalFilter filter = null)
        {
            if (_noUpdate)
                return;

            var contains = !string.IsNullOrEmpty(SenderAddressFilter);

            if (filter == null)
                filter = new RequestJournalFilter
                    (
                    DateFilter.Enabled ? DateFilter.DateBegin : null,
                    DateFilter.Enabled ? DateFilter.DateEnd : null,
                    GetClientFilter(contains),
                    GetPerformerFilter(),
                    GetProblemsFilter(),
                    GetStatusFilter(),
                    contains
                    );

            var dt = _service.GetRequests(filter.DateBegin, filter.DateEnd, filter.SenderAddress, filter.UsKey, filter.Problems, filter.Statuses, filter.SenderAddressContains);
            RequestJournal.SetRequests(dt, _timestamp);
            Requests = RequestJournal.Requests;
        }

        /*
        private void UpdateWithFiltersOld()
        {
            var problemRequests = GetProblemRequests();

            var problemRequestsWithFilter =
                ProblemsFilter.Value == null || ProblemsFilter.Value.ProblemId == _allId
                    ? null
                    : ProblemsFilter.Value.ProblemId == _allProblemsId 
                    ? problemRequests
                    : problemRequests.Where(p => p.ProblemId == ProblemsFilter.Value.ProblemId);

            if (DateFilter == null || PerformerFilter == null || ClientFilter == null)
                Requests = new ObservableCollection<Request>(RequestJournal.Requests);
            else 
                Requests = new ObservableCollection<Request>(RequestJournal.Requests
                    .Where(r => !(DateFilter.DateBegin != null && r.Date < DateFilter.DateBegin.Value) && !(DateFilter.DateEnd != null && r.Date > DateFilter.DateEnd.Value))
                    .Where(r => !(PerformerFilter.Value != null && PerformerFilter.Value.Key != -1 && r.UsKey != PerformerFilter.Value.Key))
                    .Where(r => !(ClientFilter.Value != null && !ClientFilter.Value.Equals("Все") && r.SenderAddress != null && !r.SenderAddress.Equals(ClientFilter.Value)))
                    .Where(r => !(StatusFilter.Value != null && !StatusFilter.Value.Name.Equals("Все") && r.Status.Id != StatusFilter.Value.Id))
                    .Where(r => !(SubStatusFilter.Value != null && !SubStatusFilter.Value.Name.Equals("Все") && r.SubStatus.Id != SubStatusFilter.Value.Id))
                    .Where(r => !(problemRequestsWithFilter != null && problemRequestsWithFilter.All(p => p.RequestId != r.Number)))
                    //.Where(r => !(_hideReserved && r.Status.Id == 3))
                    //.Where(r => !(_hideCancelations && r.Status.Id == 4))
                );
        }
         * */

        /*
        private void ApplyFilter(ObservableCollection<Request> requests, AbstractFilter filter)
        {
            requests.
        }
        */

        protected void InitTimer(int updatePeriodInSeconds)
        {
            _timer = new Timer(updatePeriodInSeconds * 1000) { AutoReset = false };
            _timer.Elapsed += OnTimerElapsed;
            Start();
        }

        private void Stop()
        {
            _requestStop = true;
            _timer.Stop();
        }

        private void Start()
        {
            _requestStop = false;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() => Update()); 
            
            if (!_requestStop)
            {
                _timer.Start();
            }
        }

        private int GetRequestCount(int? usKey)
        {
            return _service.GetRequestCount(
                DateFilter.Enabled ? DateFilter.DateBegin : null,
                DateFilter.Enabled ? DateFilter.DateEnd : null,
                usKey,
                !HideReserved,
                !HideCancelations
            );
        }

        private void GetCounts()
        {
            ProblemsCountAll = _service.GetProblemRequestCount(null);
            ProblemsCountSelf = _service.GetProblemRequestCount(UserId);
            
            NewRequestCount = GetRequestCount(null);
            MyRequestCount = GetRequestCount(UserId);
        }

        public void Update(bool ignoreTimestapm = false)
        {
            lock (_updateLock)
            {
                //ProblemRequestsViewModel.UpdateCounts();

                GetCounts();

                var oldTimeStamp = _timestamp;
                _timestamp = Repository.GetInstance<IRequestJournalService>().GetMaxTimeStamp();

                if (!ignoreTimestapm && _timestamp == oldTimeStamp) return;

                var selectedRequestId = SelectedItem != null ? SelectedItem.Number : -1;

                //if (RequestJournal.Requests == null)
                //RequestJournal.LoadRequests(!HideCancelations, !HideReserved, _timestamp);
                //else
                //    RequestJournal.UpdateRequests(!HideCancelations, !HideReserved, _timestamp);  
                
                //UpdateRequestIdCollection();
                
                UpdateWithFilters();

                if (Requests != null)
                    SelectedItem = Requests.FirstOrDefault(r => r.Number == selectedRequestId);
            }
        }

        protected virtual void ShowCorrespWithClient()
        {
            HideAllRows = true;
            ShowCorrespondenceWithClient = true;
            ShowCorrespondenceWithManager = false;
            SetCorrespondence(RequestMessageMod.MTC);
        }

        protected virtual void ShowCorrespWithManager()
        {
            HideAllRows = true;
            ShowCorrespondenceWithManager = true;
            ShowCorrespondenceWithClient = false;
            SetCorrespondence(RequestMessageMod.MTM);
        }

        protected void SetCorrespondence(string mod)
        {
            if (SelectedItem == null)
                return;

            SelectedItem.WatchMessages(mod);
            //Repository.GetInstance<IRequestJournalService>().WathMessages(SelectedItem.Number, mod);
        }

        public virtual void ShowDataGrid()
        {
            HideAllRows = false;
            ShowCorrespondenceWithManager = false;
            ShowCorrespondenceWithClient = false;
        }

        private void UpdateRequestIdCollection()
        {
            RequestIdCollection = new ObservableCollection<int>(RequestJournal.Requests.Select(r => r.Number));
        }
        
        public void GetUserInfo()
        {
            var serv = Repository.GetInstance<IUsersService>();
            UserId = serv.GetUserID2();
            UserName = serv.GetUserName2();
            AccessService = Repository.GetInstance<IAccessService>();
            IsSuperviser = AccessService.isSuperViser;
            
            var userService = Repository.GetInstance<IUsersService>();
            Managers = new ObservableCollection<User>(ManagerHelper.Instance.Managers);

            User = Managers.FirstOrDefault(m => m.Key == UserId);

            if (User == null)
                return;
        }

        public void TakeAJob()
        {
            SetPerformer(SelectedItem, UserName);
        }

        public void AppointPerformer()
        {
            Repository.GetInstance<IWindowsHelper>().ShowWindow(new SelectManagerViewModel(AppointPerformerOk, Managers.ToArray()), Repository.GetInstance<ISelectManagerView>());
        }

        public void AppointPerformerOk(User manager)
        {
            if(manager != null)
                SetPerformer(SelectedItem, manager.Name);
        }

        public void SetPerformer(Request request, string performer)
        {
            RequestJournal.SetPerformer(request, performer, Managers.ToArray());
            Update();
        }

        public void BindingButtonClick(int id)
        {
            PopupIsOpen = !PopupIsOpen;
        }

        protected void InitFilters()
        {
            DateFilter = new DateFilter { Name = "по дате заявки" };
           
            var clients = GetClients();
            ClientFilter = new ComboBoxFilter<string> { Name = "по клиенту", Values = clients != null ? new ObservableCollection<string>(GetClients()) : new ObservableCollection<string>() };
            ClientFilter.Values.Insert(0, "Все");
            
            PerformerFilter = new ComboBoxFilter<User> { Name = "по исполнителю" , Values = new ObservableCollection<User>(Managers) };
            PerformerFilter.Values.Insert(0, new User(-1, "Все", string.Empty, null, null, null, null, null));

            StatusFilter = new ComboBoxFilter<RequestStatus> { Name = "по статусу", Values = _statuses != null ? new ObservableCollection<RequestStatus>(_statuses) : new ObservableCollection<RequestStatus>() };
            StatusFilter.Values.Insert(0, new RequestStatus(-1, "Все", DateTime.MinValue));

            var subStatuses = GetSubStatuses();
            SubStatusFilter = new ComboBoxFilter<RequestSubStatus> { Name = "проблемные заявки", Values = subStatuses != null ? new ObservableCollection<RequestSubStatus>(subStatuses) : new ObservableCollection<RequestSubStatus>() };
            SubStatusFilter.Values.Insert(0, new RequestSubStatus(-1, -1, "Все", DateTime.MinValue));

            ProblemsFilter = new ComboBoxFilter<RequestProblem> { Name = "проблемные заявки", Values = _problems != null ? new ObservableCollection<RequestProblem>(_problems) : new ObservableCollection<RequestProblem>() };
            ProblemsFilter.Values.Insert(0, new RequestProblem(-1, _allProblemsId, "Все проблемные заявки", null));
            ProblemsFilter.Values.Insert(0, new RequestProblem(-1, _allId, "Все заявки", null));

            SetFilterCallback();
        }

        private RequestStatus[] GetStatuses()
        {
            return Repository.GetInstance<IRequestJournalService>()
                .GetAllStatuses()
                .Select()
                .Select(r => new RequestStatus(r.Field<int>("Id"), r.Field<string>("Name"), DateTime.MinValue)).ToArray();
        }

        private RequestSubStatus[] GetSubStatuses()
        {
            return Repository.GetInstance<IRequestJournalService>()
                .GetProblemSubStatuses()
                .Select()
                .Select(r => new RequestSubStatus(r.Field<int>("Id"), r.Field<int>("RequestStatusId"), r.Field<string>("Name"), DateTime.MinValue)).ToArray();
        }

        private RequestProblem[] GetRequestProblems()
        {
            return Repository.GetInstance<IRequestJournalService>()
                .GetRequestProblems()
                .Select()
                .Select(RequestProblemHelper.GetProblemRequest).ToArray();
        }

        

        private RequestProblem[] GetProblemRequests()
        {
            return Repository.GetInstance<IRequestJournalService>()
                .GetProblemRequests()
                .Select()
                .Select(RequestProblemHelper.GetRequestProblem).ToArray();
        }

        private string[] GetClients()
        {
            if (RequestJournal.Requests == null) return null;

            return
                _service.GetAllSenders(!HideCancelations, !HideReserved)
                    .Select()
                    .Select(r => r.Field<string>("SenderAddress"))
                    .ToArray();

            /*return RequestJournal.Requests.Select(r =>
            {
                var firstOrDefault = r.Messages.FirstOrDefault();
                return firstOrDefault != null ? firstOrDefault.SenderAddress : null;
            })
                .GroupBy(s => s)
                .Select(g => g.Key)
                .ToArray();*/
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<BindRequestsMessage>(this, BindRequest);
        }

        private void UnregisterMessages()
        {
            Messenger.Default.Unregister<BindRequestsMessage>(this);
        }

        public void BindRequest(BindRequestsMessage message)
        {
            var child = RequestJournal.Requests.FirstOrDefault(r => r.Number == message.ChildId);
            var parent = RequestJournal.Requests.FirstOrDefault(r => r.Number == message.ParentId);

            if (child == null || parent == null) return;

            foreach (var msg in child.Messages)
                parent.Messages.Add(msg);

            RequestJournal.Requests.Remove(child);
            UpdateRequestIdCollection();
        }

        public void ChangeSenderAddress(string newSenderAddress)
        {
            if (SelectedItem != null)
                SelectedItem.ChangeSenderAddress(newSenderAddress);
        }
    }

    /// <summary>
    /// RequestsJournalViewModel
    /// </summary>
    public class RequestsJournalViewModel : RequestsJournalViewModelBase
    {
        private CorrespondenceBaseViewModel _correspManagerViewModel;
        public CorrespondenceBaseViewModel CorrespManagerViewModel
        {
            get { return _correspManagerViewModel; }
            set { SetValue(ref _correspManagerViewModel, value); }
        }

        private CorrespondenceBaseViewModel _correspClientViewModel;
        public CorrespondenceBaseViewModel CorrespClientViewModel
        {
            get { return _correspClientViewModel; }
            set { SetValue(ref _correspClientViewModel, value); }
        }

        public override void UpdateCorrespondence()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                ((CorrespondenceRequestMtMViewModel) CorrespManagerViewModel).Update(_selectedItem);
                ((CorrespondenceRequestViewModel) CorrespClientViewModel).Update(_selectedItem);
            });
        }

        public RequestsJournalViewModel()
        {
            CorrespClientViewModel = new CorrespondenceRequestViewModel(User, null, CorrespondenceType.Client, () => Update(), ShowDataGrid);
            CorrespManagerViewModel = new CorrespondenceRequestMtMViewModel(User, null, CorrespondenceType.Manager, () => Update(), ShowDataGrid) { Managers = Managers };
            Update();
            InitFilters();
        }
    }

    /// <summary>
    /// RequestMessagesViewMode2
    /// </summary>
    public class RequestsJournalViewModel2 : RequestsJournalViewModelBase
    {
        private IRequestMessagesViewModel _requestMessagesViewModelMtC;
        public IRequestMessagesViewModel RequestMessagesViewModelMtC
        {
            get { return _requestMessagesViewModelMtC; }
            set { SetValue(ref _requestMessagesViewModelMtC, value); }
        }

        private IRequestMessagesViewModel _requestMessagesViewModelMtM;
        public IRequestMessagesViewModel RequestMessagesViewModelMtM
        {
            get { return _requestMessagesViewModelMtM; }
            set { SetValue(ref _requestMessagesViewModelMtM, value); }
        }

        private RequestNewMessageViewModel RequestNewMessageViewModel { get; set; }
        private RequestMessagesHelper _requestMessagesHelper;

        public RequestsJournalViewModel2()
        {
            _requestMessagesHelper = new RequestMessagesHelper();
            var requestNewMessageViewModelMtC = new RequestNewMessageViewModel(User, null, CorrespondenceType.Client, () => Update(), ShowDataGrid) { Managers = Managers };
            var requestNewMessageViewModelMtM = new RequestNewMessageViewModel(User, null, CorrespondenceType.Manager, () => Update(), ShowDataGrid) { Managers = Managers };
            RequestMessagesViewModelMtC = new RequestMessagesViewModelMtC(requestNewMessageViewModelMtC, ShowDataGrid);
            RequestMessagesViewModelMtM = new RequestMessagesViewModelMtM(requestNewMessageViewModelMtM, ShowDataGrid);
            InitFilters();
            Update();
            InitFilters();
        }

        public override void UpdateCorrespondence()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (_selectedItem == null) return;
                
                _selectedItem.Messages = new ObservableCollection<RequestMessage>(_requestMessagesHelper.GetMessages(_selectedItem.Number));
                RequestMessagesViewModelMtC.Update(_selectedItem);
                RequestMessagesViewModelMtM.Update(_selectedItem);
            });
        }

        protected override void ShowCorrespWithClient()
        {
            HideAllRows = true;
            ShowCorrespondenceWithClient = true;
            ShowCorrespondenceWithManager = false;
            RequestMessagesViewModelMtC.Active = true;
            RequestMessagesViewModelMtM.Active = false;
            SetCorrespondence(RequestMessageMod.MTC);
        }

        protected override void ShowCorrespWithManager()
        {
            HideAllRows = true;
            ShowCorrespondenceWithManager = true;
            ShowCorrespondenceWithClient = false;
            RequestMessagesViewModelMtC.Active = false;
            RequestMessagesViewModelMtM.Active = true;
            SetCorrespondence(RequestMessageMod.MTM);
        }

        public override void ShowDataGrid()
        {
            HideAllRows = false;
            ShowCorrespondenceWithManager = false;
            ShowCorrespondenceWithClient = false;
            RequestMessagesViewModelMtC.Active = false;
            RequestMessagesViewModelMtM.Active = false;
        }
    }
}
