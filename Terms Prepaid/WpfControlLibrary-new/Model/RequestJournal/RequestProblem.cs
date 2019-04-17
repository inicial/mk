using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Asn1.IsisMtt.Ocsp;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class RequestProblem : Data
    {
        private int _requestId;
        public int RequestId
        {
            get { return _requestId; }
            set { SetValue(ref _requestId, value); }
        }

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

        private int? _usKey;
        public int? UsKey
        {
            get { return _usKey; }
            set { SetValue(ref _usKey, value); }
        }

        public RequestProblem(int requestId, int problemId, string problemName, int? usKey)
        {
            RequestId = requestId;
            ProblemId = problemId;
            ProblemName = problemName;
            UsKey = usKey;
        }
    }
}
