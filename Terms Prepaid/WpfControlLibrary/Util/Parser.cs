using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Util
{
    public static class Parser
    {
        public static decimal? GetDecimal(object value)
        {
            string val = String.Format("{0}", value);

            decimal tmpvalue;
            decimal? result = null;
            if (decimal.TryParse(val, out tmpvalue))
                result = tmpvalue;

            return result;
        }

        public static int? GetInt(object value)
        {
            string val = String.Format("{0}", value);

            int tmpvalue;
            int? result = null;
            if (int.TryParse(val, out tmpvalue))
                result = tmpvalue;

            return result;
        }

        public static DateTime? GetDateTime(object value)
        {
            string val = String.Format("{0}", value);

            DateTime tmpvalue;
            DateTime? result = null;
            if (DateTime.TryParse(val, out tmpvalue))
                result = tmpvalue;

            return result;
        }

        public static bool? GetBool(object value)
        {
            string val = String.Format("{0}", value);

            bool tmpvalue;
            bool? result = null;
            if (bool.TryParse(val, out tmpvalue))
                result = tmpvalue;

            return result;
        }
    }
}
