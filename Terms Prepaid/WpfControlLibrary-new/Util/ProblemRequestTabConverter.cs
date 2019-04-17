using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class ProblemRequestTabConverter : IValueConverter
    {
        /// <summary>
        /// Convert boolean property "showSelfOnly" to tab selected index
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var showSelfOnly = value as bool? ?? false;
            return showSelfOnly ? 1 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var selectedTabIndex = value as int? ?? 0;
            return selectedTabIndex == 1;
        }
    }
}
