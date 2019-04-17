using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace terms_prepaid.Helper_Classes
{
    class MyTime
    {
        public int hour { get; set; }
        public int minute { get; set; }
        public MyTime(int hh, int mm)
        {

            hour = hh;
            minute = mm;
        }
        public static MyTime ParseTime(string a, string fieldname)
        {
            try
            {
                string[] strs = a.Split(':');
                int h, m;
                h = int.Parse(strs[0]);
                m = int.Parse(strs[1]);
                if (h < 0 || h > 23) { throw new Exception("Hours faled"); }
                if (m < 0 || m > 59) { throw new Exception("Minutes faled"); }
                return new MyTime(h, m);

            }
            catch (Exception)
            {
                MessageBox.Show("Не правльно заведено время \"" + fieldname + "\"", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

    }
}
