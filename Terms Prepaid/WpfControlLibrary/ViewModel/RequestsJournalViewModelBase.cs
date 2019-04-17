using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using DataService;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Utilities.DataTypes.ExtensionMethods;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Messages;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;
using System.Windows;


namespace WpfControlLibrary.ViewModel
{
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

        private int _problemsCountManager;
        public int ProblemsCountManager
        {
            get { return _problemsCountManager; }
            set { SetValue(ref _problemsCountManager, value); }
        }

        private string _problemManagerName;
        public string ProblemManagerName
        {
            get { return _problemManagerName; }
            set { SetValue(ref _problemManagerName, value); }
        }

        private UserBase _problemManager;
        public UserBase ProblemManager
        {
            get { return _problemManager; }
            set { SetValue(ref _problemManager, value); }
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

        private int _lockingCompanionsCount;
        public int LockingCompanionsCount
        {
            get { return _lockingCompanionsCount; }
            set { SetValue(ref _lockingCompanionsCount, value); }
        }

        private int _existsCompanionsCount;
        public int ExistsCompanionsCount
        {
            get { return _existsCompanionsCount; }
            set { SetValue(ref _existsCompanionsCount, value); }
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

        private ObservableCollection<RequestBase> _requests;
        public ObservableCollection<RequestBase> Requests
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
            set { SetValue(ref _hideCancelations, value); }
        }

        private bool _hideReserved;
        public bool HideReserved
        {
            get { return _hideReserved; }
            set { SetValue(ref _hideReserved, value); }
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

        protected DataRow[] _lastData;


        private bool _appendMode;
        public bool AppendMode
        {
            get { return _appendMode; }
            set { SetValue(ref _appendMode, value); }
        }
        //protected bool _appendMode;

        private string _windowTitle;
        public string WindowTitle
        {
            get { return _windowTitle; }
            set { SetValue(ref _windowTitle, value); }
        }

        private Window _journalWindow;
        public Window JournalWindow
        {
            get { return _journalWindow; }
            set { SetValue(ref _journalWindow, value); }
        }

        private bool _companionMode;
        public bool CompanionMode
        {
            get { return _companionMode; }
            set { SetValue(ref _companionMode, value); }
        }

        public void SetCompanionMode(bool new_mode)
        {
            CompanionMode = new_mode;
        }


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

        public RelayCommand ShowLockingCompanionsRequestsCmd { get; set; }
        public RelayCommand ShowExistsCompanionsRequestsCmd { get; set; }

        public RelayCommand ShowNeedReplyRequestsCmd { get; set; }

        public RelayCommand ShowAllProblemRequestsCmd { get; set; }
        public RelayCommand ShowSelfProblemRequestsCmd { get; set; }
        public RelayCommand ShowManagerProblemRequestsCmd { get; set; }

        public RelayCommand ApplySenderFilterCmd { get; set; }

        public RelayCommand CloseJournalCommand { get; set; }

        public RelayCommand BackCommand { get; set; }

        protected IRequestJournalService _service;
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

            ShowLockingCompanionsRequestsCmd = new RelayCommand(ShowLockingCompanionsRequests);
            ShowExistsCompanionsRequestsCmd = new RelayCommand(ShowExistsCompanionsRequests);

            ShowNeedReplyRequestsCmd = new RelayCommand(ShowNeedReplyRequests);

            ShowAllProblemRequestsCmd = new RelayCommand(() => ShowProblemRequests(ProblemRequestsTab.All), () => ProblemsCountAll != 0);
            ShowSelfProblemRequestsCmd = new RelayCommand(() => ShowProblemRequests(ProblemRequestsTab.Self), () => ProblemsCountSelf != 0);
            ShowManagerProblemRequestsCmd = new RelayCommand(() => ShowProblemRequests(ProblemRequestsTab.Manager), () => true);

            ApplySenderFilterCmd = new RelayCommand(ApplySenderFilter, () => !string.IsNullOrEmpty(SenderAddressFilter));

            CloseJournalCommand = new RelayCommand(CloseJournal);

            BackCommand = new RelayCommand(DoBack);

            GetUserInfo();
            ProblemRequestsViewModel = new ProblemRequestsViewModel(_userId, OnProblemGroupSelected, OnProblemManagerSelected, IsSuperviser);
            RequestJournal = new RequestJournal(() => Update());
            RegisterMessages();
            //InitTimer(10);
            InitTimer(60);

            _hideCancelations = true;
            _hideReserved = true;

            WindowTitle = "ЖУРНАЛ ЗАЯВОК";
        }

        public virtual void UpdateCorrespondence()
        {
            
        }

        private void ApplySenderFilter()
        {
            UpdateWithFilters();
        }

        private void OnProblemGroupSelected(RequestProblemGroup problemGroup)
        {
            if (problemGroup == null)
                return;

            ClearFilters();

            ProblemsFilter.SelectedValue = problemGroup.ProblemId != null 
                ? ProblemsFilter.Values.FirstOrDefault(p => p.ProblemId == problemGroup.ProblemId) 
                : ProblemsFilter.Values.Skip(1).FirstOrDefault();

            if (problemGroup.UsKey != null)
                PerformerFilter.SelectedValue = PerformerFilter.Values.FirstOrDefault(p => p.Key == problemGroup.UsKey.Value);

            Update(true);
        }

        private void OnProblemManagerSelected(UserBase user)
        {
            ProblemManager = user;
            ProblemManagerName = ProblemManager != null ? ProblemManager.Name : "";
            ProblemsCountManager = ProblemManager != null ? _service.GetProblemRequestCount(ProblemManager.Key) : 0;
        }

        private void ClearFilters()
        {
            _noUpdate = true;
            _showOnlyMyRequests = false;
            StatusFilter.Reset();
            SubStatusFilter.Reset();
            ProblemsFilter.Reset();
            ClientFilter.Reset();
            PerformerFilter.Reset();
            SenderAddressFilter = string.Empty;
            //DateFilter.Enabled = false;
            _noUpdate = false;
        }

        public void CloseJournal()
        {
            if (JournalWindow == null) return;

            JournalWindow.Close();
        }

        public void DoBack()
        {
            if (HideAllRows)
            {
                ShowDataGrid();
                return;
            }
            //else
            //{
            //    CloseJournal();
            //}
        }

        public void ShowNewRequests()
        {
            ClearFilters();
            StatusFilter.SelectedValue = StatusFilter.Values[1];
            SubStatusFilter.SelectedValues = new ObservableCollection<RequestSubStatus>(SubStatusFilter.Values.Except(new[] { SubStatusFilter.Values[15], SubStatusFilter.Values[16] }));
            Update(true);
        }

        public void ShowAllRequests()
        {
            ClearFilters();
            StatusFilter.SelectedValue = StatusFilter.Values[0];
SubStatusFilter.SelectedValues = new ObservableCollection<RequestSubStatus>(SubStatusFilter.Values.Except(new[] { SubStatusFilter.Values[15], SubStatusFilter.Values[16] }));
            Update(true);
        }

        public void ShowAllMyRequests()
        {
            ClearFilters();
            _showOnlyMyRequests = true;
            StatusFilter.SelectedValue = StatusFilter.Values[0];
SubStatusFilter.SelectedValues = new ObservableCollection<RequestSubStatus>(SubStatusFilter.Values.Except(new[] { SubStatusFilter.Values[15], SubStatusFilter.Values[16] }));
            Update(true);
        }

        public void ShowAllCompanions()
        {
            ClearFilters();
            SubStatusFilter.SelectedValues = new ObservableCollection<RequestSubStatus>(new[] { SubStatusFilter.Values[15] });
            Update(true);
        }

        public void ShowAllMyCompanions()
        {
            ClearFilters();
            _showOnlyMyRequests = true;
            SubStatusFilter.SelectedValues = new ObservableCollection<RequestSubStatus>(new[] { SubStatusFilter.Values[15] });
            Update(true);
        }

        public void ShowLockingCompanionsRequests()
        {
            ClearFilters();
            SubStatusFilter.SelectedValues = new ObservableCollection<RequestSubStatus>(new []{ SubStatusFilter.Values[15] });
            Update(true);
        }

        public void ShowExistsCompanionsRequests()
        {
            ClearFilters();
            SubStatusFilter.SelectedValues = new ObservableCollection<RequestSubStatus>(new []{ SubStatusFilter.Values[16] });
            Update(true);
        }

        private void ShowProblemRequests(ProblemRequestsTab tabIndex)
        {
            ProblemRequestsViewModel.UpdateProblems();
            ProblemRequestsViewModel.SelectedTabIndex = (int)tabIndex;
            _winHelper.ShowWindow(ProblemRequestsViewModel, Repository.GetInstance<IProblemRequestsView>(), modal: true);
        }

        private void ShowNeedReplyRequests()
        {
            ClearFilters();
            ProblemsFilter.SelectedValue = ProblemsFilter.Values[4];
            Update(true);
        }

        private void ShowCorrespondence(int columnIndex)
        {
            var managerToManagerMessagesIndex = 7;

            if (columnIndex == managerToManagerMessagesIndex)
                ShowCorrespWithManager();
            else
                ShowCorrespWithClient();
        }

        private int[] GetProblemsFilter()
        {
            return ProblemsFilter.SelectedValue == null || ProblemsFilter.SelectedValue.ProblemId == -1
                ? new int[0]
                : ProblemsFilter.SelectedValue.ProblemId == -2
                    ? _problems.Select(p => p.ProblemId).ToArray()
                    : new[] { ProblemsFilter.SelectedValue.ProblemId };
        }

        private string GetClientFilter(bool contains)
        {
            return contains 
                ? SenderAddressFilter
                : ClientFilter.SelectedValue != null && !ClientFilter.SelectedValue.Equals("Все", StringComparison.OrdinalIgnoreCase) ? ClientFilter.SelectedValue : null;
        }

        private int? GetPerformerFilter()
        {
            return PerformerFilter.SelectedValue != null && PerformerFilter.SelectedValue.Key != -1
                ? PerformerFilter.SelectedValue.Key
                : _showOnlyMyRequests ? UserId : (int?)null;
        }

        private int[] GetStatusFilter()
        {
            return StatusFilter.SelectedValue != null && StatusFilter.SelectedValue.Id != -1 ?
                new[] { StatusFilter.SelectedValue.Id } :
                _statuses.Select(s => s.Id).Where(id => !(HideReserved && id == 3) && !(HideCancelations && id == 4)).ToArray();
        }

        private int[] GetSubStatusFilter()
        {
            return SubStatusFilter.SelectedValues != null 
                ? SubStatusFilter.SelectedValues.Select(s => s.Id).ToArray()
                : SubStatusFilter.SelectedValue != null && SubStatusFilter.SelectedValue.Id != -1
                    ? new[] { SubStatusFilter.SelectedValue.Id }
                    : new int[] { };
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
                    GetSubStatusFilter(),
                    contains
                    );

            var dt = _service.GetRequests(filter.DateBegin, filter.DateEnd, filter.SenderAddress, filter.UsKey, filter.Problems, filter.Statuses, filter.SubStatuses, filter.SenderAddressContains);

            DataRow[] rows = dt.Select();

            if (AppendMode)
            {
                if (_lastData != null) rows = dt.Select().Union(_lastData).ToArray();
            }
            
            _lastData = rows.Clone() as DataRow[];

            RequestJournal.SetRequests(rows, _timestamp);
            
            var requests = new List<RequestBase>(RequestJournal.Requests);

            var dates =
                requests.GroupBy(r => r.DateDay)
                    .Select(g => new { g.Key, g.FirstOrDefault().Caption })
                    .Select(o => new RequestBase { Date = new DateTime(o.Key.Year, o.Key.Month, o.Key.Day, 23, 59, 59), Caption = o.Caption + "_x", IsData = true });

            var orderedRequestsWithDates = requests
                .Concat(dates)
                .OrderByDescending(r => r.Date);
                /*.GroupBy(r => r.DateDay)
                .SelectMany(g => g.OrderBy(r => r.Date));*/

            Requests = new ObservableCollection<RequestBase>(orderedRequestsWithDates);
        }

        protected void InitTimer(int updatePeriodInSeconds)
        {
            _timer = new Timer(updatePeriodInSeconds * 1000) { AutoReset = false };
            _timer.Elapsed += OnTimerElapsed;
            Start();
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
            ProblemsCountManager = ProblemManager != null ? _service.GetProblemRequestCount(ProblemManager.Key) : 0;

            NewRequestCount = GetRequestCount(null);
            MyRequestCount = GetRequestCount(UserId);

            LockingCompanionsCount = _service.GetLockingCompanionsCount();
            ExistsCompanionsCount = _service.GetExistsCompanionsCount();
        }

        public void Update(bool ignoreTimestapm = false)
        {
            lock (_updateLock)
            {
                GetCounts();

                var oldTimeStamp = _timestamp;
                _timestamp = Repository.GetInstance<IRequestJournalService>().GetMaxTimeStamp();

                if (!ignoreTimestapm && _timestamp == oldTimeStamp) return;

                var selectedRequestId = SelectedItem != null ? SelectedItem.Number : -1;

                UpdateWithFilters();

                if (Requests != null)
                    SelectedItem = (Request)Requests.FirstOrDefault(r => RequestIdTest(r, selectedRequestId));
            }
        }

        private bool RequestIdTest(RequestBase request, int selectedRequestId)
        {
            if (!(request is Request))
                return false;

            return((Request) request).Number == selectedRequestId;
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
            UserId = serv.GetUsKey();
            UserName = serv.GetUserName2();
            AccessService = Repository.GetInstance<IAccessService>();
            IsSuperviser = AccessService.isSuperViser;
            
            Managers = new ObservableCollection<User>(ManagerHelper.Instance.Managers);

            User = Managers.FirstOrDefault(m => m.Key == UserId);
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
            DateFilter = new DateFilter { Name = "по дате заявки", DateBegin = DateTime.Now.AddMonths(-1), Enabled = true };
           
            var clients = GetClients();
            ClientFilter = new ComboBoxFilter<string> { Name = "по клиенту", Values = clients != null ? new ObservableCollection<string>(GetClients()) : new ObservableCollection<string>() };
            ClientFilter.Values.Insert(0, "Все");
            
            PerformerFilter = new ComboBoxFilter<User> { Name = "по исполнителю" , Values = new ObservableCollection<User>(Managers) };
            PerformerFilter.Values.Insert(0, new User(-1, "Все", string.Empty, null, null, null, null, null));

            StatusFilter = new ComboBoxFilter<RequestStatus> { Name = "статистика", Values = _statuses != null ? new ObservableCollection<RequestStatus>(_statuses) : new ObservableCollection<RequestStatus>() };
            StatusFilter.Values.Insert(0, new RequestStatus(-1, "Все", DateTime.MinValue));

            var subStatuses = GetSubStatuses();
            SubStatusFilter = new ComboBoxFilter<RequestSubStatus> { Name = "подстатусы", Values = subStatuses != null ? new ObservableCollection<RequestSubStatus>(subStatuses) : new ObservableCollection<RequestSubStatus>() };
            SubStatusFilter.Values.Insert(0, new RequestSubStatus(-1, -1, "Все", DateTime.MinValue));
            //for (int i = 0; i < SubStatusFilter.Values.Count; i++ )
            //{
            //    RequestSubStatus req = SubStatusFilter.Values[i];
            //    int i1 = req.Id;
            //    int i2 = req.RequestStatusId;
            //    string s = req.Name;
            //    TpLogger.WriteException("SUBSTATUS[" + i.ToString() + "]: ID=" + req.Id.ToString() + ", RequestStatusId=" + req.RequestStatusId.ToString() + ", Name=" + req.Name);
            //}

            ProblemsFilter = new ComboBoxFilter<RequestProblem> { Name = "проблемные заявки", Values = _problems != null ? new ObservableCollection<RequestProblem>(_problems) : new ObservableCollection<RequestProblem>() };
            ProblemsFilter.Values.Insert(0, new RequestProblem(-1, _allProblemsId, "Все проблемные заявки", null));
            ProblemsFilter.Values.Insert(0, new RequestProblem(-1, _allId, "Все заявки", null));
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

        private string[] GetClients()
        {
            if (RequestJournal.Requests == null) return null;

            return
                _service.GetAllSenders(!HideCancelations, !HideReserved)
                    .Select()
                    .Select(r => r.Field<string>("SenderAddress"))
                    .ToArray();
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<BindRequestsMessage>(this, BindRequest);
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
}