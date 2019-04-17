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
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.ViewModel
{
    public class ProblemRequestsViewModel : Data
    {
        private readonly int _usKey;
        private readonly IRequestJournalService _service;
        private readonly Action<RequestProblemGroup> _selectProblemHandler;

        public RelayCommand<RequestProblemGroup> SelectProblemCmd { get; set; }

        private bool _showSelfOnly;
        public bool ShowSelfOnly
        {
            get { return _showSelfOnly; }
            set { SetValue(ref _showSelfOnly, value); }
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

        public ProblemRequestsViewModel(int usKey, Action<RequestProblemGroup> selectProblemHandler)
        {
            _usKey = usKey;
            _selectProblemHandler = selectProblemHandler;
            _service = Repository.GetInstance<IRequestJournalService>();

            SelectProblemCmd = new RelayCommand<RequestProblemGroup>(p =>
            {
                if(_selectProblemHandler != null)
                    _selectProblemHandler.Invoke(p);
            });
        }

        public void UpdateCounts()
        {
            ProblemsCountAll = _service.GetProblemRequestCount(null);
            ProblemsCountSelf = _service.GetProblemRequestCount(_usKey);
        }

        public void UpdateProblems()
        {
            ProblemGroupsAll = new ObservableCollection<RequestProblemGroup>(_service.GetProblemRequestGroups(null).Select().Select(GetRequestProblemGroup));
            ProblemGroupsSelf = new ObservableCollection<RequestProblemGroup>(_service.GetProblemRequestGroups(_usKey).Select().Select(GetRequestProblemGroup));
        }

        private RequestProblemGroup GetRequestProblemGroup(DataRow row)
        {
            return new RequestProblemGroup(row.Field<int>("ProblemId"), row.Field<string>("ProblemName"), row.Field<int>("Count"));
        }

        /*
        private void LoadProblemsOld()
        {
            var problemRequests = _service.GetProblemRequests().Select();
            var problems = _service
                .GetRequestProblems()
                .Select()
                .Select(p => GetProblemGroupEx(p, problemRequests
                    .Where(r => r.Field<int>("ProblemId") == p.Field<int>("ProblemId"))));

            //ProblemGroupsAll = new ObservableCollection<RequestProblemGroup>(problems.Where(g => g.));
        }

        private RequestProblemGroupEx GetProblemGroupEx(DataRow row, IEnumerable<DataRow> requests)
        {
            var problem = RequestProblemHelper.GetRequestProblem(row);
            var problemRequests = requests.Select(RequestProblemHelper.GetProblemRequest);
            return new RequestProblemGroupEx(problem.ProblemId, problem.ProblemName, problemRequests);
        }
         * */
    }
}
