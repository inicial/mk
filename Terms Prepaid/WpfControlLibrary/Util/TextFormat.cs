using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Util
{
    public static class TextFormat
    {

        private static string GetDateString(DateTime date)
        {
            return date.ToString(@"HH:mm \/ dd.MM.yy");
        }

        public static string GetDate(DateTime? date)
        {
            return date != null ? GetDateString((DateTime)date) : "";
        }

        public static string GetDate(DateTime? date, DateTime defaultDate)
        {
            DateTime nDate = date ?? defaultDate;
            return GetDateString(nDate);
        }

        public static string GetCost(decimal cost, string rate)
        {
            return cost != 0 ? string.Format("{0:#0} {1}", cost, rate) : "0"; 
        }
    }
}
