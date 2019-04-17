using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfControlLibrary.Common;

namespace WpfControlLibrary.Util
{
    public class BronNumberConverter : ReplaceSomeValuesConverter
    {
        public BronNumberConverter()
        {
            Values = new Dictionary<string, string>
            {
                {"ERROR", DesktopMessages.SystemError},
                {"DELAYED", "Бронь создана"}
            };
            ComparisonType = StringComparison.OrdinalIgnoreCase;
        }
    }
}
