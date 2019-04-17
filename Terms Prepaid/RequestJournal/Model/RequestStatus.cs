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

        public RequestStatus(int id, string name, DateTime date) { Id = id; Name = name; Date = date; }
    }
}