using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Model.Voucher
{
    public class ServiceSettingInfo : Data
    {
        private string _name;
        private string _value;
        private string _manager;
        private string _date;
        private DateTime? _dateTime;

        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }

        public string Value
        {
            get { return _value; }
            set {  SetValue(ref _value, value); }
        }

        public string Manager
        {
            get { return _manager; }
            set {  SetValue(ref _manager, value); }
        }

        public string Date
        {
            get { return _date; }
            set {  SetValue(ref _date, value); }
        }

        public DateTime? DateTime
        {
            get { return _dateTime; }
            set { SetValue(ref _dateTime, value); }
        }

        public ServiceSettingInfo(string name, string value, string manager, string date)
        {
            Name = name;
            Value = value;
            Manager = manager;
            Date = date;
        }

        public ServiceSettingInfo(string name, RowSettingDate row)
        {
            Name = name;
            Value = TextFormat.GetDate(row.DateValue);
            Manager = row.Manager;
            Date = row.DateChange;
            DateTime = row.DateValue;
        }

        private DateTime? GetLesserDate(DateTime? date1, DateTime? date2)
        {
            if (date1 != null && date2 != null)
            {
                var d1 = (DateTime) date1;
                var d2 = (DateTime) date2;
                
                if (d1.Date == d2.Date)
                    return d1 < d2 ? d1 : d2;
            }
            return date1;
        }

        public ServiceSettingInfo(string name, RowSettingDate row, DateTime? date2)
        {
            Name = name;
            Value = TextFormat.GetDate(GetLesserDate(row.DateValue, date2));
            Manager = row.Manager;
            Date = row.DateChange;
            DateTime = row.DateValue;
        }

        public ServiceSettingInfo(string name, RowSettingValue row, string rate)
        {
            Name = name;
            Value = TextFormat.GetCost(row.NumericValue, rate);
            Manager = row.Manager;
            Date = row.DateChange;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ServiceSettingInfo;
            if (other == null)
                return false;

            return string.Equals(_value, other._value) && string.Equals(_manager, other._manager) && string.Equals(_date, other._date);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_value != null ? _value.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_manager != null ? _manager.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (_date != null ? _date.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
