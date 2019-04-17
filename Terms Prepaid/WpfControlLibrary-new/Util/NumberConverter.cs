using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfControlLibrary.Util
{
    public class NumberConverter : IValueConverter
    {
        public delegate string PostFunction(string val);
        public delegate object PreFunction(object value);

        public PostFunction PostFunc { get; set; }
        public PreFunction PreFunc { get; set; }

        public string Prefix { get; set; }
        public string Postfix { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object val = PreFunc != null ? PreFunc.Invoke(value) : value;

            int? num = Parser.GetInt(val);
            if (num != null)
            {
                StringBuilder sb = new StringBuilder();
                string result = sb.Append(Prefix)
                    .Append(num.ToString())
                    .Append(Postfix)
                    .Append(NumEnding.GetEnding((int)num))
                    .ToString();

                return PostFunc != null ? PostFunc.Invoke(result) : result;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
