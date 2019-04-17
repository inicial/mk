using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Messages
{
    public class MessageBase : Data
    {
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { SetValue(ref _date, value); }
        }
    }
}
