using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public class NameValueManagerDate : NameValue
    {
        private object _manager;
        public object Manager
        {
            get { return _manager; }
            set { SetValue(ref _manager, value); }
        }

        private object _date;
        public object Date
        {
            get { return _date; }
            set { SetValue(ref _date, value); }
        }

        public NameValueManagerDate(object name, object value) : base(name, value)
        {

        }

        public NameValueManagerDate(object name, object value, object manager, object date)
            : base(name, value)
        {
            Manager = manager;
            Date = date;
        }
    }
}
