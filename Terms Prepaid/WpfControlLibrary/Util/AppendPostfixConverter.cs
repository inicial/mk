using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using Utilities.DataTypes.ExtensionMethods;

namespace WpfControlLibrary.Util
{
    public class AppendPostfixConverter : IValueConverter
    {
        public string Base { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var objs = value as IEnumerable<object>;

            if (objs == null)
                return null;

            var count = objs.Count();
            var postfix = count == 1 ? "" : count.Between(2, 4) ? "а" : "ов";
            
            return String.Format("{0} {1}{2}", count, Base, postfix);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
