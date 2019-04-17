using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public class NameValue : Data
    {
        private object _name;
        public object Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        private object _value;
        public object Value
        {
            get { return _value; }
            set { SetValue(ref _value, value); }
        }

        public NameValue(object name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
