using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    public class RequestBase : Data
    {
        private string _caption;
        public string Caption
        {
            get { return _caption; }
            set { SetValue(ref _caption, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetValue(ref _date, value); }
        }

        private bool _isData;
        public bool IsData
        {
            get { return _isData; }
            set { SetValue(ref _isData, value); }
        }

        public DateTime DateDay { get { return new DateTime(Date.Year, Date.Month, Date.Day); } }
    }
}
