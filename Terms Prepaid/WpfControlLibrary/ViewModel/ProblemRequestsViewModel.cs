using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using DataService;
using GalaSoft.MvvmLight.Command;
using WpfControlLibrary.Common;
using WpfControlLibrary.Helpers;
using WpfControlLibrary.Model.Common;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.ViewModel
{
    public enum ProblemRequestsTab
    {
        All = 0,
        Self = 1,
        Manager = 2
    }
    
    public class ProblemRequestsViewModel : Data
    {
        private readonly int _usKey;
        private readonly IRequestJournalService _service;
        private readonly IUsersService _userService;
        private readonly Action<RequestProblemGroup> _selectProblemHandler;
        private readonly Action<UserBase> _selectProblemManager;

        public RelayCommand<RequestProblemGroup> SelectProblemCmd { get; private set; }
        public RelayCommand SelectManagerCmd { get; private set; }

        private bool _showSelfOnly;
        public bool ShowSelfOnly
        {
            get { return _showSelfOnly; }
            set { SetValue(ref _showSelfOnly, value); }
        }

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { SetValue(ref _selectedTabIndex, value); }
        }

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

        private bool _isSuperviser;
        public bool IsSuperviser
        {
            get { return _isSuperviser; }
            set { SetValue(ref _isSuperviser, value); }
        }

        private UserBase _selectedManager;
        public UserBase SelectedManager
        {
            get { return _selectedManager; }
            set { SetValue(ref _selectedManager, value); }
        }

        private ObservableCollection<RequestProblemGroup> _problemGroupsAll;
        public ObservableCollection<RequestProblemGroup> ProblemGroupsAll
        {
            get { return _problemGroupsAll; }
            set { SetValue(ref _problemGroupsAll, value); }
        }

        private ObservableCollection<RequestProblemGroup> _problemGroupsSelf;
        public ObservableCollection<RequestProblemGroup> ProblemGroupsSelf
        {
            get { return _problemGroupsSelf; }
            set { SetValue(ref _problemGroupsSelf, value); }
        }

        private ObservableCollection<RequestProblemGroup> _problemGroupsManager;
        public ObservableCollection<RequestProblemGroup> ProblemGroupsManager
        {
            get { return _problemGroupsManager; }
            set { SetValue(ref _problemGroupsManager, value); }
        }

        private ObservableCollection<UserBase> _managersWithProblems;
        public ObservableCollection<UserBase> ManagersWithProblems
        {
            get { return _managersWithProblems; }
            set { SetValue(ref _managersWithProblems, value); }
        }

        public ProblemRequestsViewModel(int usKey, Action<RequestProblemGroup> selectProblemHandler, Action<UserBase> selectProblemManager, bool isSuperviser)
        {
            _usKey = usKey;
            IsSuperviser = isSuperviser;
            _selectProblemHandler = selectProblemHandler;
            _selectProblemManager = selectProblemManager;
            _service = Repository.GetInstance<IRequestJournalService>();
            _userService = Repository.GetInstance<IUsersService>();

            LoadManagersWithProblems();

            SelectProblemCmd = new RelayCommand<RequestProblemGroup>(p =>
            {
                if(_selectProblemHandler != null)
                    _selectProblemHandler.Invoke(p);
            });

            SelectManagerCmd = new RelayCommand(UpdateManagerProblems);
        }

        public void LoadManagersWithProblems()
        {
            ManagersWithProblems = new ObservableCollection<UserBase>(_service
                .GetManagerWithProblemRequests()
                .Select()
                .Select(r => new UserBase(r.Field<int>("key"), r.Field<string>("name"))));

            SelectedManager = ManagersWithProblems.FirstOrDefault();
            UpdateManagerProblems();
        }

        public void UpdateManagerProblems()
        {
            ProblemGroupsManager = SelectedManager != null 
                ? GetRequestProblemGroupCollection(SelectedManager.Key) 
                : new ObservableCollection<RequestProblemGroup>();

            if(_selectProblemManager != null)
                _selectProblemManager.Invoke(SelectedManager);
        }

        public void UpdateCounts()
        {
            ProblemsCountAll = _service.GetProblemRequestCount(null);
            ProblemsCountSelf = _service.GetProblemRequestCount(_usKey);
        }

        public void UpdateProblems()
        {
            ProblemGroupsAll = GetRequestProblemGroupCollection(null);
            ProblemGroupsSelf = GetRequestProblemGroupCollection(_usKey);
            UpdateManagerProblems();
        }

        private ObservableCollection<RequestProblemGroup> GetRequestProblemGroupCollection(int? usKey)
        {
            var problemGroups = new List<RequestProblemGroup>(
                _service.GetProblemRequestGroups(usKey).Select().Select(r => GetRequestProblemGroup(r, usKey)));
            var all = new RequestProblemGroup(null, "Все", problemGroups.Sum(g => g.Count), usKey);
            problemGroups.Insert(0, all);

            return new ObservableCollection<RequestProblemGroup>(problemGroups);
        }

        private RequestProblemGroup GetRequestProblemGroup(DataRow row, int? usKey)
        {
            return new RequestProblemGroup(row.Field<int>("ProblemId"), row.Field<string>("ProblemName"), row.Field<int>("Count"), usKey);
        }
    }
}
