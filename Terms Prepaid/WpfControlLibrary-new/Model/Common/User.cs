using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.Common
{
    public class User : Data
    {
        private int _key;
        public int Key
        {
            get { return _key; }
            set { SetValue(ref _key, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetValue(ref _email, value); }
        }

        private int? _phone;
        public int? Phone
        {
            get { return _phone; }
            set { SetValue(ref _phone, value); }
        }

        private string _fName;
        public string FName
        {
            get { return _fName; }
            set { SetValue(ref _fName, value); }
        }

        private string _sName;
        public string SName
        {
            get { return _sName; }
            set { SetValue(ref _sName, value); }
        }

        private string _mName;
        public string MName
        {
            get { return _mName; }
            set { SetValue(ref _mName, value); }
        }

        private string _job;
        public string Job
        {
            get { return _job; }
            set { SetValue(ref _job, value); }
        }

        private string _departament;
        public string Departament
        {
            get { return _departament; }
            set { SetValue(ref _departament, value); }
        }

        public User(int key, string name, string email, int? phone, string fName, string sName, string mName, string job)
        {
            Key = key;
            Name = name;
            Email = email;
            Phone = phone;
            FName = fName;
            SName = sName;
            MName = mName;
            Job = job;
        }
    }
}
