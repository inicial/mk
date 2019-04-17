using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Common
{
    public class Person : Data
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetValue(ref _firstName, value); }
        }

        private string _secondName;
        public string SecondName
        {
            get { return _secondName; }
            set { SetValue(ref _secondName, value); }
        }

        private string _middleName;
        public string MiddleName
        {
            get { return _middleName; }
            set { SetValue(ref _middleName, value); }
        }
    }
}
