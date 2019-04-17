using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class RequestProblemGroup : Data
    {
        private int _problemId;
        public int ProblemId
        {
            get { return _problemId; }
            set { SetValue(ref _problemId, value); }
        }

        private string _problemName;
        public string ProblemName
        {
            get { return _problemName; }
            set { SetValue(ref _problemName, value); }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { SetValue(ref _count, value); }
        }

        public RequestProblemGroup(int problemId, string probleName, int count)
        {
            ProblemId = problemId;
            ProblemName = probleName;
            Count = count;
        }
    }

    public class RequestProblemGroupEx : RequestProblemGroup
    {
        private bool _selfOnly;
        public bool SelfOnly
        {
            get { return _selfOnly; }
            set { SetValue(ref _selfOnly, value); }
        }

        private ObservableCollection<RequestProblem> _requestProblems;
        public ObservableCollection<RequestProblem> RequestProblems
        {
            get { return _requestProblems; }
            set
            {
                SetValue(ref _requestProblems, value);
                Count = _requestProblems != null ? _requestProblems.Count : 0;
            }
        }

        public RequestProblemGroupEx(int problemId, string problemName, IEnumerable<RequestProblem> problems)
            : base(problemId, problemName, 0)
        {
            RequestProblems = new ObservableCollection<RequestProblem>(problems);
        }
    }
}
