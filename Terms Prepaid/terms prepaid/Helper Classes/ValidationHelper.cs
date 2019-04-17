using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace terms_prepaid.Helper_Classes
{
    public static class ValidationHelper
    {
        private static char[] AcceptChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', (char)8 };

        public static bool Validate(string text, char symbol)
        {
            if (!Array.Exists(AcceptChars, x => x == symbol))
                return false;

            if (symbol == ',' && (text.IndexOf(',') >= 0 || text.Length < 1))
                return false;

            return true;
        }

    }
}
