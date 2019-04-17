using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.Util
{
    public class RequestNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var request = value as Request;

            if(request == null)
                return null;

            return request.DgCode ?? request.Number.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
