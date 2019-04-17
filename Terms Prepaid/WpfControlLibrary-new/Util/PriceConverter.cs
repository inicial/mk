using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public enum Currency { Eu, Ru, Us }

    public class PriceConverter : IMultiValueConverter
    {
        public static string GetStringValue(decimal value, Currency currency, decimal rate)
        {
            decimal val = ((decimal)value * (decimal)rate);

            switch (currency)
            {
                case Currency.Eu:
                    return String.Format("{0} \u20AC", val.ToString("D"));

                case Currency.Ru:
                    return String.Format("{0} руб", val.ToString("D"));

                case Currency.Us:
                    return String.Format("${0}", val.ToString("D"));
            }

            return null;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            decimal? value = values[0] as decimal?;
            Currency? currency = values[1] as Currency?;
            decimal? rate = values[2] as decimal?;

            if (value != null && currency != null && rate != null)
            {
                return GetStringValue((decimal)value, (Currency)currency, (decimal)rate);
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
