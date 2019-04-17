using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using terms_prepaid.Helpers;

namespace terms_prepaid.Helper_Classes
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


        public static string GetDate(ChangeData pdate)
        {
            return pdate != null ? GetDate(pdate.Date) : "";
        }
    }
}
