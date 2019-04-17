using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class SingleValueConverter : IValueConverter
    {

        protected string _oldValue;
        public string OldValue
        {
            get { return _oldValue; }
            set { _oldValue = value; }
        }

        protected string _newValue;
        public string NewValue
        {
            get { return _newValue; }
            set { _newValue = value; }
        }

        protected StringComparison _comparisonType = StringComparison.Ordinal;
        public StringComparison ComparisonType
        {
            get { return _comparisonType; }
            set { _comparisonType = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.ToString().Equals(OldValue, _comparisonType))
                return NewValue;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
