using System;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class RequestProblemGroup : Data
    {
        private int? _problemId;
        public int? ProblemId
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

        private int? _usKey;
        public int? UsKey
        {
            get { return _usKey; }
            set { SetValue(ref _usKey, value); }
        }

        public RequestProblemGroup(int? problemId, string probleName, int count, int? usKey)
        {
            ProblemId = problemId;
            ProblemName = probleName;
            Count = count;
            UsKey = usKey;
        }
    }
}
