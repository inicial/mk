using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using WpfControlLibrary.Model.Voucher;

namespace WpfControlLibrary.Util
{
    public class TransferDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var direction = value as TransferService.TransferDirection? ?? TransferService.TransferDirection.Unknown;

            switch (direction)
            {
                case TransferService.TransferDirection.Before:
                    return "туда";

                case TransferService.TransferDirection.After:
                    return "обратно";

                case TransferService.TransferDirection.Both:
                    return "туда и обратно";

                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
