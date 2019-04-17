using System;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Model.RequestJournal
{
    /// <summary>
    /// Статус заявки
    /// </summary>
    public class RequestStatus : Data
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetValue(ref _id, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetValue(ref _date, value); }
        }

        private int? _usKey;
        public int? UsKey
        {
            get { return _usKey; }
            set { SetValue(ref _usKey, value); }
        }

        private string _managerName;
        public string ManagerName
        {
            get { return _managerName; }
            set { SetValue(ref _managerName, value); }
        }

        public RequestStatus(int id, string name, DateTime date, int? usKey = null, string managerName = null) 
        { 
            Id = id; 
            Name = name; 
            Date = date;
            UsKey = usKey;
            ManagerName = managerName;
        }
    }
}