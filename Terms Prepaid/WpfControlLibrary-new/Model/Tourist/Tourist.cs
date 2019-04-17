using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Model.Common;

namespace WpfControlLibrary.Model.Tourist
{
    public class Tourist : Person
    {
        private DateTime? _birtday;
        public DateTime? Birtday
        {
            get { return _birtday; }
            set { SetValue(ref _birtday, value); }
        }

        private int _key;
        public int Key
        {
            get { return _key; }
            set { SetValue(ref _key, value); }
        }
    }
}
