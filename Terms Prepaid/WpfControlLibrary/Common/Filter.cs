using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Common
{
    public abstract class AbstractFilter : Data
    {
        public Action Callback { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetValue(ref _name, value);
                if(Callback != null) Callback.Invoke();
            }
        }
    }

    public class TextFilter : AbstractFilter
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                SetValue(ref _text, value);
                if (Callback != null) Callback.Invoke();
            }
        }
    }

    public class ComboBoxFilter<T> : AbstractFilter
    {
        private T _selectedValue;
        public T SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                SetValue(ref _selectedValue, value);
                if (Callback != null) Callback.Invoke();
            }
        }

        private ObservableCollection<T> _selectedValues;
        public ObservableCollection<T> SelectedValues
        {
            get { return _selectedValues; }
            set { SetValue(ref _selectedValues, value); }
        }

        private ObservableCollection<T> _values;
        public ObservableCollection<T> Values
        {
            get { return _values; }
            set
            {
                SetValue(ref _values, value);
                if (Callback != null) Callback.Invoke();
            }
        }

        public void Reset()
        {
            SelectedValue = Values.FirstOrDefault();
            SelectedValues = null;
        }
    }

    public class DateFilter : AbstractFilter
    {
        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                SetValue(ref _enabled, value);
                if(_enabled) 
                    Enable();
                else 
                    Disable();
                if (Callback != null) 
                    Callback.Invoke();
            }
        }

        private DateTime? _dateBegin;
        public DateTime? DateBegin
        {
            get { return _dateBegin; }
            set
            {
                SetValue(ref _dateBegin, value);
                DateBeginForQuery = _dateBegin ?? DateTime.MinValue;
                if (Callback != null) Callback.Invoke();
            }
        }

        private DateTime? _dateEnd;
        public DateTime? DateEnd
        {
            get { return _dateEnd; }
            set
            {
                var date = value != null ? ((DateTime) value).AddSeconds(86399) : value; // Adding 23 hours 59 min 59 sec
                SetValue(ref _dateEnd, date);
                DateEndForQuery = _dateEnd ?? DateTime.MaxValue;
                if (Callback != null) Callback.Invoke();
            }
        }

        public DateTime DateBeginForQuery { get; private set; }
        public DateTime DateEndForQuery { get; private set; }

        public DateFilter(bool enabled = true)
        {
            Enabled = enabled;
        }

        public DateFilter(DateTime begin, DateTime end, bool enabled = true)
        {
            Enabled = enabled;
            DateBegin = begin;
            DateEnd = end;
        }

        private void Enable()
        {
            
            DateBeginForQuery = _dateBegin ?? DateTime.MinValue;
            DateEndForQuery = _dateEnd ?? DateTime.MaxValue;
        }

        private void Disable()
        {
            DateBeginForQuery = DateTime.MinValue;
            DateEndForQuery = DateTime.MaxValue;
        }
        
    }
}
