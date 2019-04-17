using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class ReplaceSomeValuesConverter : IValueConverter
    {
        protected Dictionary<string, string> _values;
        public Dictionary<string, string> Values
        {
            get { return _values; }
            set { _values = value; }
        }

        protected StringComparison _comparisonType = StringComparison.Ordinal;
        public StringComparison ComparisonType
        {
            get { return _comparisonType; }
            set { _comparisonType = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            
            foreach (var v in _values)
                if (value.ToString().Equals(v.Key, _comparisonType))
                    return v.Value;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
