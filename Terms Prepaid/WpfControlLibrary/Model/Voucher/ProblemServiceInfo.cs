using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Voucher
{
    public class ProblemServiceInfo : Data, ICloneable
    {
        private string _problemName;
        public int Rownum { get; set; }
        public string DgCode { get; set; }
        public int DlKey { get; set; }
        public int SvKey { get; set; }
        public int ProblemCode { get; set; }

        public string ProblemName
        {
            get { return _problemName; }
            set { SetValue(ref _problemName, value); }
        }

        public string ServiceName { get; set; }

        public ProblemServiceInfo(string dgCode, int dlKey, int svKey, int problemCode, string problemName, string serviceName)
        {
            DgCode = dgCode;
            DlKey = dlKey;
            SvKey = svKey;
            ProblemCode = problemCode;
            ProblemName = problemName;
            ServiceName = serviceName;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
