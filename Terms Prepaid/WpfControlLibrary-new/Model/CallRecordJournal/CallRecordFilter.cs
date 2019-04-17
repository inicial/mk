using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.CallRecordJournal
{
    public class CallRecordFilter : Data
    {
        public Action<CallRecordFilter> _updateHandler { get; set; }

        private DateTime? _dateBegin;
        public DateTime? DateBegin
        {
            get { return _dateBegin; }
            set { SetValue(ref _dateBegin, value); }
        }

        private DateTime? _dateEnd;
        public DateTime? DateEnd
        {
            get { return _dateEnd; }
            set { SetValue(ref _dateEnd, value); }
        }

        private KeyValuePair<int,string> _status;
        public KeyValuePair<int,string> Status
        {
            get { return _status; }
            set { SetValue(ref _status, value); }
        }

        private ObservableCollection<KeyValuePair<int,string>> _statuses;
        public ObservableCollection<KeyValuePair<int, string>> Statuses
        {
            get { return _statuses; }
            set { SetValue(ref _statuses, value); }
        }

        public CallRecordFilter(IEnumerable<KeyValuePair<int, string>> statuses, Action<CallRecordFilter> updateHandler = null)
        {
            Statuses = new ObservableCollection<KeyValuePair<int, string>>(statuses);
            _updateHandler = updateHandler;
        }

        public void Set(DateTime? dateBegin, DateTime? dateEnd, int? statusId)
        {
            DateBegin = dateBegin;
            DateEnd = dateEnd;
            Status = Statuses.FirstOrDefault(s => s.Key == statusId);
        }

        public void Update()
        {
            if(_updateHandler != null)
                _updateHandler.Invoke(this);
        }
    }
}
