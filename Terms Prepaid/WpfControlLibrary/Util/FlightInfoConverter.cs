using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class FlightInfoConverter : IMultiValueConverter
    {
        public static string GetStringValue2(decimal value, int currency, int priceType)
        {
            string prefix = "";
            
            switch (priceType)
            {
                case 1:
                    prefix = "от";
                    break;

                default:
                    prefix = "";
                    break;
            }
            
            switch (currency)
            {
                case 1:
                    return String.Format("{0} \u20AC{1:## ###} ", prefix, value);

                case 2:
                    return String.Format("{0} {1:## ###} р", prefix, value);

                default:
                    return String.Format("{0} ${1:## ###}", prefix, value);
            }

            //return String.Format("{0} ${1}", prefix, value);

            //return "";
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal? value = Parser.GetDecimal(values[0]);
            int? currency = Parser.GetInt(values[1]);
            int? priceType = Parser.GetInt(values[2]);

            
            if (value != null)
            {
                if (currency != null && priceType != null)
                    return GetStringValue2((decimal) value, (int) currency, (int) priceType);
                return String.Format("{0}", value);
            }
            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
