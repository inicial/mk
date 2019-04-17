using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Util
{
    public static class NumEnding
    {
        public static string GetEnding(int num)
        {
            switch (num)
            {
                case 1:
                    return "";

                case 2:
                case 3:
                case 4:
                    return "а";

                default:
                    return "ов";
            }
        }
    }
}
