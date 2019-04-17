using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public struct Value
    {
        public string OldValue;
        public string NewValue;

        public Value(string oldValue, string newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public class CityCountryConverter : IValueConverter
    {
        private static readonly IList<Value> _dict = new ReadOnlyCollection<Value>
        (new[] {
             new Value ("Москва", "М"),
             new Value ("Российская Федерация","РФ")
        });

        public static String Convert(string str)
        {
            return str != null ? _dict.Aggregate(str, (current, val) => current.Replace(val.OldValue, val.NewValue)) : null;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return _dict.Aggregate<Value, string>(value.ToString(),
                    (current, val) => current.Replace(val.OldValue, val.NewValue));
            }
            catch (Exception)
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
