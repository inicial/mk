using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO.Packaging;
using System.Linq;
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

namespace WpfControlLibrary.ViewModel
{
    public interface IRequestsJournalViewModel
    {
        
    }

    public class RequestsJournalViewModelBase : ViewModelBase, IRequestsJournalViewModel
    {
        private Timer _timer;
        private volatile bool _requestStop = false;

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

        private string _userSignature;
        public string UserSignature
        {
            get { return _userSignature; }
            set { SetValue(ref _userSignature, value); }
        }

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetValue(ref _user, value); }
        }

        ulong _timestamp;

        protected ISimpleWindows _appointPerformerWindows;

        public RelayCommand TakeAJobCommand { get; set; }
        public RelayCommand AppointPerformerCommand { get; set; }
        public RelayCommand<int> BindingButtonCommand { get; set; }
        public RelayCommand ShowCorrespWithClientCommand { get; set; }
        public RelayCommand ShowCorrespWithManagerCommand { get; set; }
        public RelayCommand ShowDataGridCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        public RequestsJournalViewModelBase()
        {
            TakeAJobCommand = new RelayCommand(TakeAJob, () => BindButtonEnabledConverter.GetBindPermission(SelectedItem));
            AppointPerformerCommand = new RelayCommand(AppointPerformer, () => BindButtonEnabledConverter.GetBindPermission(SelectedItem));
            BindingButtonCommand = new RelayCommand<int>(BindingButtonClick);
            ShowCorrespWithClientCommand = new RelayCommand(ShowCorrespWithClient, () => SelectedItem != null);
            ShowCorrespWithManagerCommand = new RelayCommand(ShowCorrespWithManager, () => SelectedItem != null);
            ShowDataGridCommand = new RelayCommand(ShowDataGrid);
            UpdateCommand = new RelayCommand(Update);

            GetUserInfo();
            RequestJournal = new RequestJournal(Update, Managers);
            RegisterMessages();
            InitTimer(5);
        }

        public virtual void UpdateCorrespondence()
        {
            
        }

        private void SetFilterCallback()
        {
            //MessageStatusFilter.Callback = UpdateWithFilters;
            StatusFilter.Callback = UpdateWithFilters;
            ClientFilter.Callback = UpdateWithFilters;
            PerformerFilter.Callback = UpdateWithFilters;
            DateFilter.Callback = UpdateWithFilters;
        }

        private void UpdateWithFilters()
        {
            if (DateFilter == null || PerformerFilter == null || ClientFilter == null)
                Requests = new ObservableCollection<Request>(RequestJournal.Requests);
            else 
                Requests = new ObservableCollection<Request>(RequestJournal.Requests
                    .Where(r => 
                    !(DateFilter.DateBegin != null && r.Date < DateFilter.DateBegin.Value)
                    && !(DateFilter.DateEnd != null && r.Date > DateFilter.DateEnd.Value))
                    .Where(r => !(PerformerFilter.Value != null && PerformerFilter.Value.Key != -1 && r.UsKey != PerformerFilter.Value.Key))
                    .Where(r => !(ClientFilter.Value != null && !ClientFilter.Value.Equals("Все") && !r.Messages.FirstOrDefault().SenderAddress.Equals(ClientFilter.Value)))
                    .Where(r => !(StatusFilter.Value != null && !StatusFilter.Value.Name.Equals("Все") && r.RequestStatuses.FirstOrDefault(s => s.Id == StatusFilter.Value.Id) == null))
                );
        }

        /*
        private void ApplyFilter(ObservableCollection<Request> requests, AbstractFilter filter)
        {
            requests.
        }
        */

        protected void InitTimer(int updatePeriodInSeconds)
        {
            _timer = new Timer(updatePeriodInSeconds * 1000);
            _timer.AutoReset = false;
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
            DispatcherHelper.CheckBeginInvokeOnUI(Update);
            
            if (!_requestStop)
            {
                _timer.Start();
            }
        }

        public void Update()
        {
            var oldTimeStamp = _timestamp;
            
            _timestamp = Repository.GetInstance<IRequestJournalService>().GetMaxTimeStamp();

            if (_timestamp == oldTimeStamp) return;

            var selectedRequestId = SelectedItem != null ? SelectedItem.Number : -1;

            //RequestJournal.LoadNewMessages();
            RequestJournal.GetRequests();
            UpdateRequestIdCollection();

            UpdateWithFilters();

            if (Requests != null)
                SelectedItem = Requests.FirstOrDefault(r => r.Number == selectedRequestId);
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

        protected virtual void ShowDataGrid()
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
            UserSignature = serv.GetUserSignature();
            AccessService = Repository.GetInstance<IAccessService>();
            IsSuperviser = AccessService.isSuperViser;
            
            var userService = Repository.GetInstance<IUsersService>();
            Managers = new ObservableCollection<User>(userService.GetManagerList()
                .Select()
                .Select(row => new User(row.Field<int>("us_key"), row.Field<string>("US_FullNameLat"), row.Field<string>("US_MAILBOX"), row.Field<int?>("Phone"))));

            User = Managers.FirstOrDefault(m => m.Key == UserId);

            UserSignature = UserSignature
                .Replace("[username]", UserName)
                .Replace("[mail]", User.Email)
                .Replace("[phone]", Convert.ToString(User.Phone));
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
            PerformerFilter.Values.Insert(0, new User(-1, "Все", string.Empty, null));

            var statuses = GetStatuses();
            StatusFilter = new ComboBoxFilter<RequestStatus> { Name = "по статусу", Values = statuses != null ? new ObservableCollection<RequestStatus>(statuses) : new ObservableCollection<RequestStatus>() };

            StatusFilter.Values.Insert(0, new RequestStatus(-1, "Все", DateTime.MinValue));

            SetFilterCallback();
        }

        private RequestStatus[] GetStatuses()
        {
            return Repository.GetInstance<IRequestJournalService>()
                .GetAllStatuses()
                .Select()
                .Select(r => new RequestStatus(r.Field<int>("Id"), r.Field<string>("Name"), DateTime.MinValue)).ToArray();
        }

        private string[] GetClients()
        {
            if (RequestJournal.Requests == null) return null;

            return RequestJournal.Requests.Select(r => r.Messages.FirstOrDefault().SenderAddress)
                .GroupBy(s => s)
                .Select(g => g.Key)
                .ToArray();
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
            ((CorrespondenceRequestMtMViewModel)CorrespManagerViewModel).Update(_selectedItem);
            ((CorrespondenceRequestViewModel)CorrespClientViewModel).Update(_selectedItem);
        }

        public RequestsJournalViewModel()
        {
            CorrespClientViewModel = new CorrespondenceRequestViewModel(User, null, CorrespondenceType.Client, Update, ShowDataGrid) { UserSignature = UserSignature };
            CorrespManagerViewModel = new CorrespondenceRequestMtMViewModel(User, null, CorrespondenceType.Manager, Update, ShowDataGrid) { UserSignature = UserSignature, Managers = Managers };
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

        public RequestsJournalViewModel2()
        {
            var requestNewMessageViewModelMtC = new RequestNewMessageViewModel(User, null, CorrespondenceType.Client, Update, ShowDataGrid) { UserSignature = UserSignature, Managers = Managers };
            var requestNewMessageViewModelMtM = new RequestNewMessageViewModel(User, null, CorrespondenceType.Manager, Update, ShowDataGrid) { UserSignature = UserSignature, Managers = Managers };
            RequestMessagesViewModelMtC = new RequestMessagesViewModelMtC(requestNewMessageViewModelMtC);
            RequestMessagesViewModelMtM = new RequestMessagesViewModelMtM(requestNewMessageViewModelMtM);
            Update();
            InitFilters();
        }

        public override void UpdateCorrespondence()
        {
            RequestMessagesViewModelMtC.Update(_selectedItem);
            RequestMessagesViewModelMtM.Update(_selectedItem);
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

        protected override void ShowDataGrid()
        {
            HideAllRows = false;
            ShowCorrespondenceWithManager = false;
            ShowCorrespondenceWithClient = false;
            RequestMessagesViewModelMtC.Active = false;
            RequestMessagesViewModelMtM.Active = false;
        }
    }
}
