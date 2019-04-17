using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class PriceConverter2 : IMultiValueConverter
    {
        public static string GetStringValue(decimal value, Currency currency)
        {
            switch (currency)
            {
                case Currency.Eu:
                    return String.Format("{0} \u20AC");

                case Currency.Ru:
                    return String.Format("{0} руб");

                case Currency.Us:
                    return String.Format("{0} $");
            }

            return null;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            decimal? value = values[0] as decimal?;
            Currency? currency = values[1] as Currency?;

            if (value != null && currency != null)
            {
                return GetStringValue((decimal)value, (Currency)currency);
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
