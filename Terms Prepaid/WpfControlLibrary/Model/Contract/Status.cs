using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Contract
{
    public class Status : Data
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetValue(ref _date, value); }
        }

        private string _lbStatus;
        public string LbStatus
        {
            get { return _lbStatus; }
            set { SetValue(ref _lbStatus, value); }
        }

        private string _lbStatusD;
        public string LbStatusD
        {
            get { return _lbStatusD; }
            set { SetValue(ref _lbStatusD, value); }
        }

        private string _lbStatusM;
        public string LbStatusM
        {
            get { return _lbStatusM; }
            set { SetValue(ref _lbStatusM, value); }
        }
    }
}
