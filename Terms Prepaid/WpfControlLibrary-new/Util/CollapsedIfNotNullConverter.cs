using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.Util
{
    public class CollapsedIfNotNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var msg = value as RequestMessage;
            if (msg == null)
                return Visibility.Collapsed;

            return !msg.IsIncomming && !msg.Mod.Equals(RequestMessageMod.COM, StringComparison.OrdinalIgnoreCase) && msg.Sent == null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
